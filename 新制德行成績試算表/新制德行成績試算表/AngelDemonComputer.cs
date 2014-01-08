using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SmartSchool.Customization.Data;
using SmartSchool.Customization.Data.StudentExtension;
using IntelliSchool.DSA30.Util;
using SmartSchool.Feature.ScoreCalcRule;
//using System.Threading;

namespace 德行成績試算表
{
    public class AngelDemonComputer
    {
        //private enum RoundMode { 四捨五入, 無條件進位, 無條件捨去 }
        private static decimal GetRoundScore(decimal score, int decimals, WearyDogComputer.RoundMode mode)
        {
            return WearyDogComputer.GetRoundScore(score, decimals, mode);
        }


        private DSXmlHelper _MoralConductHelper;
        private List<UsefulPeriodAbsence> _UsefulPeriodAbsences;
        private List<string> _NoabsenceList = new List<string>();

        private Dictionary<string, decimal> _degreeList;
        private XmlElement _MoralConductElement;

        public string ParseLevel(decimal score)
        {
            foreach (string var in _degreeList.Keys)
            {
                if (_degreeList[var] <= score)
                    return var;
            }
            return "";
        }

        public AngelDemonComputer()
        {
            //取得德行成績計算規則

            _MoralConductElement = QueryScoreCalcRule.GetMoralConductCalcRule();
            if (_MoralConductElement != null)
                _MoralConductHelper = new DSXmlHelper(_MoralConductElement);
            else
                _MoralConductHelper = new DSXmlHelper();

            #region 取得記分缺曠項目

            #region 取得節次類別與缺曠對照表
            List<string> periodList = new List<string>();
            List<string> absenceList = new List<string>();

            //取得節次對照表
            foreach (XmlElement var in SmartSchool.Feature.Basic.Config.GetPeriodList().GetContent().GetElements("Period"))
            {
                string name = var.GetAttribute("Type");
                if (!periodList.Contains(name))
                    periodList.Add(name);
            }
            //取得假別對照表
            foreach (XmlElement var in SmartSchool.Feature.Basic.Config.GetAbsenceList().GetContent().GetElements("Absence"))
            {
                //假別清單
                string name = var.GetAttribute("Name");
                if (!absenceList.Contains(name))
                    absenceList.Add(name);
                //建立不影響全勤清單
                bool noabsence;
                if (bool.TryParse(var.GetAttribute("Noabsence"), out noabsence) && noabsence && !_NoabsenceList.Contains(name))
                    _NoabsenceList.Add(name);
            }
            #endregion
            _UsefulPeriodAbsences = new List<UsefulPeriodAbsence>();
            foreach (XmlElement element in _MoralConductHelper.GetElements("PeriodAbsenceCalcRule/Rule"))
            {
                string absence = element.GetAttribute("Absence");
                string period = element.GetAttribute("Period");
                decimal subtract;
                decimal.TryParse(element.GetAttribute("Subtract"), out subtract);
                int aggregated;
                int.TryParse(element.GetAttribute("Aggregated"), out aggregated);
                if (aggregated > 0 && subtract > 0 && periodList.Contains(period) && absenceList.Contains(absence))
                {
                    _UsefulPeriodAbsences.Add(new UsefulPeriodAbsence(absence, period, subtract, aggregated));
                }
            }
            #endregion

            SystemInformation.getField("Degree");
            _degreeList = (Dictionary<string, decimal>)SystemInformation.Fields["Degree"];
        }

        public List<UsefulPeriodAbsence> UsefulPeriodAbsences
        {
            get { return _UsefulPeriodAbsences; }
        }

        public List<string> TextScoreList
        {
            get 
            {
                List<string> vTextScoreList = new List<string>();

                SmartSchool.Customization.Data.SystemInformation.getField("文字評量對照表");

                System.Xml.XmlElement ElmTextScoreList=(System.Xml.XmlElement)SmartSchool.Customization.Data.SystemInformation.Fields["文字評量對照表"];

                foreach (System.Xml.XmlNode Node in ElmTextScoreList.SelectNodes("Content/Morality"))
                    vTextScoreList.Add(Node.Attributes["Face"].InnerText);

                return vTextScoreList; 
            }
        }

        public void FillDemonScore(AccessHelper dataSeed, int schoolyear, int semester, List<StudentRecord> students)
        {

            dataSeed.StudentHelper.FillSemesterMoralScore(true, students);
            dataSeed.StudentHelper.FillReward(schoolyear, semester, students);
            dataSeed.StudentHelper.FillAttendance(schoolyear, semester, students);

            XmlDocument doc = new XmlDocument();
            foreach (StudentRecord student in students)
            {
                XmlElement element = doc.CreateElement("DemonScore");
                XmlElement subScoreElement;
                decimal subScore;
                decimal finalScore = 0;
                //精準位數
                int decimals = 2;
                if (!int.TryParse(_MoralConductHelper.GetText("BasicScore/@Decimals"), out decimals))
                    decimals = 2;
                //進位模式
                WearyDogComputer.RoundMode mode = WearyDogComputer.RoundMode.四捨五入;
                switch (_MoralConductHelper.GetText("BasicScore/@DecimalType"))
                {
                    default:
                    case "四捨五入":
                        mode = WearyDogComputer.RoundMode.四捨五入;
                        break;
                    case "無條件捨去":
                        mode = WearyDogComputer.RoundMode.無條件捨去;
                        break;
                    case "無條件進位":
                        mode = WearyDogComputer.RoundMode.無條件進位;
                        break;
                }
                //超過一百分以一百分計
                bool limit100 = _MoralConductHelper.GetText("BasicScore/@Over100") == "以100分計";
                #region 處理獎懲
                //銷過紀錄是否計算
                bool calcCancel = false;
                if (!bool.TryParse(_MoralConductHelper.GetText("RewardCalcRule/@CalcCancel"), out calcCancel))
                    calcCancel = false;
                #region 統計獎懲次數
                int AwardACount = 0;
                int AwardBCount = 0;
                int AwardCCount = 0;
                int FaultACount = 0;
                int FaultBCount = 0;
                int FaultCCount = 0;
                bool hasUltimateAdmonition = false;
                foreach (RewardInfo reward in student.RewardList)
                {
                    if (reward.SchoolYear != schoolyear || reward.Semester != semester)
                        continue;
                    if (!reward.Cleared || calcCancel)
                    {
                        AwardACount += reward.AwardA;
                        AwardBCount += reward.AwardB;
                        AwardCCount += reward.AwardC;
                        FaultACount += reward.FaultA;
                        FaultBCount += reward.FaultB;
                        FaultCCount += reward.FaultC;
                        hasUltimateAdmonition |= reward.UltimateAdmonition;
                    }
                }
                #endregion
                #region 處理基分
                subScoreElement = doc.CreateElement("SubScore");
                subScore = 0;
                subScoreElement.SetAttribute("Type", "基分");
                if (hasUltimateAdmonition)
                {
                    subScoreElement.SetAttribute("Status", "留校查看");
                    decimal.TryParse(_MoralConductHelper.GetText("BasicScore/@UltimateAdmonitionScore"), out subScore);
                    subScoreElement.SetAttribute("Score", "" + subScore);
                }
                else
                {
                    subScoreElement.SetAttribute("Status", "一般生");
                    decimal.TryParse(_MoralConductHelper.GetText("BasicScore/@NormalScore"), out subScore);
                    subScoreElement.SetAttribute("Score", "" + subScore);
                }
                element.AppendChild(subScoreElement);
                finalScore += subScore;
                #endregion
                #region 計算獎懲項目成績
                #region 處理大功
                if (AwardACount > 0)
                {
                    subScoreElement = doc.CreateElement("SubScore");
                    subScore = 0;
                    subScoreElement.SetAttribute("Type", "獎懲");
                    subScoreElement.SetAttribute("Name", "大功");
                    subScoreElement.SetAttribute("Count", "" + AwardACount);
                    decimal addScore = 0;
                    for (int i = 0; i < AwardACount; i++)
                    {
                        decimal newscore;
                        if (decimal.TryParse(_MoralConductHelper.GetText("RewardCalcRule/@AwardA" + (i + 1)), out newscore))
                            addScore = newscore;
                        subScore += addScore;
                    }
                    subScoreElement.SetAttribute("Score", "" + subScore);
                    element.AppendChild(subScoreElement);
                    finalScore += subScore;
                }
                #endregion
                #region 處理小功
                if (AwardBCount > 0)
                {
                    subScoreElement = doc.CreateElement("SubScore");
                    subScore = 0;
                    subScoreElement.SetAttribute("Type", "獎懲");
                    subScoreElement.SetAttribute("Name", "小功");
                    subScoreElement.SetAttribute("Count", "" + AwardBCount);
                    decimal addScore = 0;
                    for (int i = 0; i < AwardBCount; i++)
                    {
                        decimal newscore;
                        if (decimal.TryParse(_MoralConductHelper.GetText("RewardCalcRule/@AwardB" + (i + 1)), out newscore))
                            addScore = newscore;
                        subScore += addScore;
                    }
                    subScoreElement.SetAttribute("Score", "" + subScore);
                    element.AppendChild(subScoreElement);
                    finalScore += subScore;
                }
                #endregion
                #region 處理嘉獎
                if (AwardCCount > 0)
                {
                    subScoreElement = doc.CreateElement("SubScore");
                    subScore = 0;
                    subScoreElement.SetAttribute("Type", "獎懲");
                    subScoreElement.SetAttribute("Name", "嘉獎");
                    subScoreElement.SetAttribute("Count", "" + AwardCCount);
                    decimal addScore = 0;
                    for (int i = 0; i < AwardCCount; i++)
                    {
                        decimal newscore;
                        if (decimal.TryParse(_MoralConductHelper.GetText("RewardCalcRule/@AwardC" + (i + 1)), out newscore))
                            addScore = newscore;
                        subScore += addScore;
                    }
                    subScoreElement.SetAttribute("Score", "" + subScore);
                    element.AppendChild(subScoreElement);
                    finalScore += subScore;
                }
                #endregion
                #region 處理大過
                if (FaultACount > 0)
                {
                    subScoreElement = doc.CreateElement("SubScore");
                    subScore = 0;
                    subScoreElement.SetAttribute("Type", "獎懲");
                    subScoreElement.SetAttribute("Name", "大過");
                    subScoreElement.SetAttribute("Count", "" + FaultACount);
                    decimal addScore = 0;
                    for (int i = 0; i < FaultACount; i++)
                    {
                        decimal newscore;
                        if (decimal.TryParse(_MoralConductHelper.GetText("RewardCalcRule/@FaultA" + (i + 1)), out newscore))
                            addScore = newscore * (-1);
                        subScore += addScore;
                    }
                    subScoreElement.SetAttribute("Score", "" + subScore);
                    element.AppendChild(subScoreElement);
                    finalScore += subScore;
                }
                #endregion
                #region 處理小過
                if (FaultBCount > 0)
                {
                    subScoreElement = doc.CreateElement("SubScore");
                    subScore = 0;
                    subScoreElement.SetAttribute("Type", "獎懲");
                    subScoreElement.SetAttribute("Name", "小過");
                    subScoreElement.SetAttribute("Count", "" + FaultBCount);
                    decimal addScore = 0;
                    for (int i = 0; i < FaultBCount; i++)
                    {
                        decimal newscore;
                        if (decimal.TryParse(_MoralConductHelper.GetText("RewardCalcRule/@FaultB" + (i + 1)), out newscore))
                            addScore = newscore * (-1);
                        subScore += addScore;
                    }
                    subScoreElement.SetAttribute("Score", "" + subScore);
                    element.AppendChild(subScoreElement);
                    finalScore += subScore;
                }
                #endregion
                #region 處理警告
                if (FaultCCount > 0)
                {
                    subScoreElement = doc.CreateElement("SubScore");
                    subScore = 0;
                    subScoreElement.SetAttribute("Type", "獎懲");
                    subScoreElement.SetAttribute("Name", "警告");
                    subScoreElement.SetAttribute("Count", "" + FaultCCount);
                    decimal addScore = 0;
                    for (int i = 0; i < FaultCCount; i++)
                    {
                        decimal newscore;
                        if (decimal.TryParse(_MoralConductHelper.GetText("RewardCalcRule/@FaultC" + (i + 1)), out newscore))
                            addScore = newscore * (-1);
                        subScore += addScore;
                    }
                    subScoreElement.SetAttribute("Score", "" + subScore);
                    element.AppendChild(subScoreElement);
                    finalScore += subScore;
                }
                #endregion
                #endregion
                #endregion
                #region 處理缺曠
                Dictionary<string, int> attendanceCount = new Dictionary<string, int>();
                bool noabsence = true;
                foreach (UsefulPeriodAbsence u in _UsefulPeriodAbsences)
                {
                    attendanceCount.Add(u.Period + "_" + u.Absence, 0);
                }
                foreach (AttendanceInfo attendance in student.AttendanceList)
                {
                    if (attendance.SchoolYear != schoolyear || attendance.Semester != semester)
                        continue;
                    //假別次數
                    if (attendanceCount.ContainsKey(attendance.PeriodType + "_" + attendance.Absence))
                        attendanceCount[attendance.PeriodType + "_" + attendance.Absence]++;
                    //全勤判斷
                    if (!_NoabsenceList.Contains(attendance.Absence))
                        noabsence = false;
                }
                //填入加減分缺曠
                foreach (UsefulPeriodAbsence u in _UsefulPeriodAbsences)
                {
                    if (attendanceCount[u.Period + "_" + u.Absence] > 0)
                    {
                        subScoreElement = doc.CreateElement("SubScore");
                        subScore = 0;
                        subScore = u.Subtract * (attendanceCount[u.Period + "_" + u.Absence] / u.Aggregated * (-1));
                        subScoreElement.SetAttribute("Type", "缺曠");
                        subScoreElement.SetAttribute("Absence", u.Absence);
                        subScoreElement.SetAttribute("PeriodType", u.Period);
                        subScoreElement.SetAttribute("Count", "" + attendanceCount[u.Period + "_" + u.Absence]);
                        subScoreElement.SetAttribute("Score", "" + subScore);
                        element.AppendChild(subScoreElement);
                        finalScore += subScore;
                    }
                }
                //填入全勤加分
                if (noabsence)
                {
                    subScoreElement = doc.CreateElement("SubScore");
                    subScore = 0;
                    decimal.TryParse(_MoralConductHelper.GetText("PeriodAbsenceCalcRule/@NoAbsenceReward"), out subScore);
                    subScoreElement.SetAttribute("Type", "全勤");
                    subScoreElement.SetAttribute("Score", "" + subScore);
                    element.AppendChild(subScoreElement);
                    finalScore += subScore;
                }
                #endregion
                #region 處理加減分及評語
                foreach (SemesterMoralScoreInfo moralscore in student.SemesterMoralScoreList)
                {
                    //是這學期的
                    if (moralscore.SchoolYear == schoolyear && moralscore.Semester == semester)
                    {
                        foreach (XmlElement each in moralscore.Detail.SelectNodes("TextScore/Morality"))
                        {
                            subScoreElement = doc.CreateElement("SubScore");
                            subScoreElement.SetAttribute("Type", "文字評量");

                            string face = each.GetAttribute("Face");
                            string comment = each.InnerText;

                            subScoreElement.SetAttribute("Face", face);
                            subScoreElement.SetAttribute("FaceText", comment);

                            element.AppendChild(subScoreElement);
                        }

                        //導師加減分
                        subScoreElement = doc.CreateElement("SubScore");
                        subScore = 0;
                        subScore = moralscore.SupervisedByDiff;
                        subScoreElement.SetAttribute("Type", "加減分");
                        subScoreElement.SetAttribute("DiffItem", "導師加減分");
                        subScoreElement.SetAttribute("Score", "" + subScore);
                        element.AppendChild(subScoreElement);
                        finalScore += subScore;
                        #region 其他加減分
                        if (moralscore.OtherDiff != null)
                        {
                            foreach (string diffItem in moralscore.OtherDiff.Keys)
                            {
                                subScoreElement = doc.CreateElement("SubScore");
                                subScore = 0;
                                subScore = moralscore.OtherDiff[diffItem];
                                subScoreElement.SetAttribute("Type", "加減分");
                                subScoreElement.SetAttribute("DiffItem", diffItem);
                                subScoreElement.SetAttribute("Score", "" + subScore);
                                element.AppendChild(subScoreElement);
                                finalScore += subScore;
                            }
                        }
                        #endregion
                        #region 評語
                        subScoreElement = doc.CreateElement("Others");
                        subScoreElement.SetAttribute("Comment", moralscore.SupervisedByComment);
                        element.AppendChild(subScoreElement);
                        #endregion
                    }
                }
                #endregion
                element.SetAttribute("RealScore", "" + finalScore);
                element.SetAttribute("Score", "" + GetRoundScore(
                    (limit100 && finalScore > 100) ? 100 : finalScore,//超過一百以一百分計
                    decimals,
                    mode));
                student.Fields.Add("DemonScore", element);
            }
        }
    }
}