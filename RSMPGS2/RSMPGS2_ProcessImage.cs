using System;
using System.Collections;
using System.Threading;
using System.IO;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace nsRSMPGS
{

  public class cAggregatedStatusEvent
  {
    public string sTimeStamp;
    public string sMessageId;
    public string sBitStatus;
    public string sFunctionalPosition;
    public string sFunctionalState;
  }

  public class cCommandEvent
  {
    public string sTimeStamp;
    public string sMessageId;
    public string sEvent;
    public string sCommandCodeId;
    public string sName;
    public string sCommand;

    public object oValue;
    public string sAge;
  }

  public class cStatusEvent
  {
    public string sTimeStamp;
    public string sMessageId;
    public string sEvent;
    public string sStatusCodeId;
    public string sName;
    public string sStatus;
    public string sUpdateRate;
    public bool bUpdateOnChange;
    public string sQuality;
  }

  public class cStatusSubscription
  {
    public string sStatusCodeId;
    public string sName;
    public string sUpdateRate;
  }

  public partial class cProcessImage
  {
    
    public void LoadProcessImageValues(string FileName)
    {

      try
      {
        if (!File.Exists(FileName))
        {
          var hFile = File.Create(FileName);
          hFile.Close();
        }
      }
      catch (Exception e)
      {
        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Failed to create file {0}", e.Message, FileName);
      }

      try
      {
        StreamReader swReferenceFile = new StreamReader((System.IO.Stream)File.OpenRead(FileName), Encoding.UTF8);


        string sLine;

        while ((sLine = swReferenceFile.ReadLine()) != null)
        {
          cRoadSideObject RoadSideObject;
          sLine = sLine.Trim();
          if (sLine.StartsWith(";") || sLine.Length == 0 || sLine.StartsWith("["))
          {
            continue;
          }
          try
          {
            RoadSideObject = RSMPGS.JSon.JSonSerializer.Deserialize<cRoadSideObject>(sLine);
          }
          catch
          {
            continue;
          }

          cRoadSideObject gRoadSide = cHelper.FindRoadSideObject(RoadSideObject.sNTSObjectId, RoadSideObject.sComponentId, false);

          if (gRoadSide != null)
          {
            gRoadSide.bBitStatus = RoadSideObject.bBitStatus;

            //foreach (cAlarmEvent AlarmEvent in RoadSideObject.AlarmEvents)
            //{
            //  gRoadSide.AlarmEvents.Add(AlarmEvent);
            //}
            foreach (cCommandEvent CommandEvent in RoadSideObject.CommandEvents)
            {
              gRoadSide.CommandEvents.Add(CommandEvent);
            }
            foreach (cStatusEvent StatusEvent in RoadSideObject.StatusEvents)
            {
              gRoadSide.StatusEvents.Add(StatusEvent);
            }
            foreach (cAggregatedStatusEvent AggregatedStatusEvent in RoadSideObject.AggregatedStatusEvents)
            {
              gRoadSide.AggregatedStatusEvents.Add(AggregatedStatusEvent);
            }
            foreach (cStatusObject StatusObject in RoadSideObject.StatusObjects)
            {
              if (StatusObject.StatusReturnValues != null && StatusObject.StatusReturnValues.Count > 0)
              {
                cStatusObject StatusObject2 = gRoadSide.getStatusObject(StatusObject.sStatusCodeId); // .StatusReturnValues[0].sStatusCommandId);
                if (StatusObject2 != null)
                {
                  foreach (cStatusReturnValue StatusReturnValues in StatusObject.StatusReturnValues)
                  {
                    cStatusReturnValue StatusReturnValues2 = StatusObject2.getStatusReturnValueByName(StatusReturnValues.sName);
                    if (StatusReturnValues2 != null)
                    {
                      StatusReturnValues2.sLastUpdateRate = StatusReturnValues.sLastUpdateRate;
                      StatusReturnValues2.bLastUpdateOnChange = StatusReturnValues.bLastUpdateOnChange;
                      StatusReturnValues2.Value.SetValue(StatusReturnValues.Value.GetValue());
                      StatusReturnValues2.sQuality = StatusReturnValues.sQuality;
                    }
                  }
                }
              }

            }
            //foreach (cAlarmObject AlarmObject in RoadSideObject.AlarmObjects)
            //{
            //    cAlarmObject AlarmObject2 = gRoadSide.getAlarmObject(AlarmObject.sAlarmCodeId);
            //    if (AlarmObject2 != null)
            //    {
            //        AlarmObject2.bAcknowledged = AlarmObject.bAcknowledged;
            //        AlarmObject2.bActive = AlarmObject.bActive;
            //        AlarmObject2.bSuspended = AlarmObject.bSuspended;
            //    }
            //}

            foreach (cCommandObject CommandObject in RoadSideObject.CommandObjects)
            {
              if (CommandObject.CommandReturnValues != null && CommandObject.CommandReturnValues.Count > 0)
              {
                cCommandObject CommandObject2 = gRoadSide.getCommandObject(CommandObject.sCommandCodeId);
                if (CommandObject2 != null)
                {
                  foreach (cCommandReturnValue CommandReturnValue in CommandObject.CommandReturnValues)
                  {
                    cCommandReturnValue CommandReturnValue2 = CommandObject2.getCommandReturnValueByName(CommandReturnValue.sName);
                    if (CommandReturnValue2 != null)
                    {
                      CommandReturnValue2.sLastRecAge = CommandReturnValue.sLastRecAge;
                      CommandReturnValue2.sLastRecValue = CommandReturnValue.sLastRecValue;
                      CommandReturnValue2.sAge = CommandReturnValue.sAge;
                      CommandReturnValue2.Value.SetValue(CommandReturnValue.Value.GetValue());
                    }
                  }
                }
              }

            }
          }
          else
          {
            //System.Windows.Forms.MessageBox.Show("Did not find roadside object");
          }
        }

        swReferenceFile.Close();
      }
      catch (Exception e)
      {
        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Failed to open file {0}", e.Message, FileName);
      }
    }


    public void SaveProcessImageValues()
    {

      string FileName = cPrivateProfile.SettingsPath() + "\\" + "ProcessImage.dat";

      try
      {

        StreamWriter swProcessImageFile = new StreamWriter((System.IO.Stream)File.OpenWrite(FileName), Encoding.UTF8);

        swProcessImageFile.WriteLine(";");
        swProcessImageFile.WriteLine("; ProcessImage stored " + DateTime.Now.ToString());
        swProcessImageFile.WriteLine(";");

        swProcessImageFile.WriteLine("[General]");
        swProcessImageFile.WriteLine("");
        foreach (cRoadSideObject RoadSideObject in RSMPGS.ProcessImage.RoadSideObjects.Values)
        {
          swProcessImageFile.WriteLine(RSMPGS.JSon.JSonSerializer.SerializeObject(RoadSideObject));
        }
        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Stored Process Image to '{0}'", FileName);
        swProcessImageFile.Close();
      }
      catch (Exception e)
      {
        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Process Image could not be stored to '{0}'", FileName, e.Message);
      }

    }

    public void ResetProcessImage()
    {
      foreach (cRoadSideObject RoadSideObject in RSMPGS.ProcessImage.RoadSideObjects.Values)
      {

        RoadSideObject.StatusEvents.Clear();
        RoadSideObject.CommandEvents.Clear();
        RoadSideObject.AggregatedStatusEvents.Clear();
        RoadSideObject.bBitStatus = null;
        RoadSideObject.sFunctionalPosition = null;
        RoadSideObject.sFunctionalState = null;

        //foreach (cAlarmObject AlarmObject in RoadSideObject.AlarmObjects)
        //{
        //    AlarmObject.bAcknowledged = false;
        //    AlarmObject.bActive = false;
        //    AlarmObject.bSuspended = false;
        //}

        foreach (cStatusObject StatusObject in RoadSideObject.StatusObjects)
        {
          foreach (cStatusReturnValue StatusReturnValues in StatusObject.StatusReturnValues)
          {
            StatusReturnValues.sQuality = null;
            StatusReturnValues.Value.SetInitialUnknownValue();
            StatusReturnValues.sLastUpdateRate = null;
            StatusReturnValues.bLastUpdateOnChange = false;
          }
        }
        foreach (cCommandObject CommandObject in RoadSideObject.CommandObjects)
        {
          foreach (cCommandReturnValue CommandReturnValue in CommandObject.CommandReturnValues)
          {
            CommandReturnValue.sLastRecAge = null;
            CommandReturnValue.sLastRecValue = null;
          }
        }
        foreach (cAlarmObject AlarmObject in RoadSideObject.AlarmObjects)
        {
          AlarmObject.AlarmEvents.Clear();
          AlarmObject.bAcknowledged = false;
          AlarmObject.bActive = false;
          AlarmObject.bSuspended = false;
        }
      }
    }

    public void ReSendSubscriptions(bool bSubscribe)
    {
      foreach (cRoadSideObject RoadSideObject in RoadSideObjects.Values)
      {

        List<RSMP_Messages.StatusSubscribe_Status_Over_3_1_4> StatusSubscribe_Values = new List<RSMP_Messages.StatusSubscribe_Status_Over_3_1_4>();

        foreach (cStatusObject StatusObject in RoadSideObject.StatusObjects)
        {
          foreach (cStatusReturnValue StatusReturnValue in StatusObject.StatusReturnValues)
          {

            if (StatusReturnValue.sLastUpdateRate != null)
            {

              RSMP_Messages.StatusSubscribe_Status_Over_3_1_4 StatusSubscribe_Value = new RSMP_Messages.StatusSubscribe_Status_Over_3_1_4();
              StatusSubscribe_Value.sCI = StatusReturnValue.StatusObject.sStatusCodeId;
              StatusSubscribe_Value.n = StatusReturnValue.sName;
              StatusSubscribe_Value.uRt = StatusReturnValue.sLastUpdateRate;
              StatusSubscribe_Value.sOc = StatusReturnValue.bLastUpdateOnChange;

              StatusSubscribe_Values.Add(StatusSubscribe_Value);
            }
          }
        }
        if (StatusSubscribe_Values.Count > 0)
        {
          if (bSubscribe)
          {
            RSMPGS.JSon.CreateAndSendSubscriptionMessage(RoadSideObject, StatusSubscribe_Values);
          }
          else
          {
            RSMPGS.JSon.CreateAndSendStatusMessage(RoadSideObject, StatusSubscribe_Values, "StatusUnsubscribe");
          }
        }
      }
    }

    public void CyclicCleanup(int iElapsedMillisecs)
    {

    }

  }

}
