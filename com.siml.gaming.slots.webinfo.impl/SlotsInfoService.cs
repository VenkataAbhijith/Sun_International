using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Configuration;
using System.Data.SqlClient;
using com.siml.gaming.slots.webinfo.contract;

using System.Xml.Serialization;

using log4net;
using log4net.Config;

using System.Web;

using System.Net;

using svc = www.suninternational.com.schemas.SunIntSchemas.gaming.property.SlotsInfo.v1._01;

namespace com.siml.gaming.slots.webinfo.impl
{
    [ServiceBehavior(
        //ConcurrencyMode = ConcurrencyMode.Single,
        //InstanceContextMode = InstanceContextMode.Single,
        //ReleaseServiceInstanceOnTransactionComplete = true,
      Namespace = "http://www.suninternational.com/gaming/web/SlotsInfoService.v101"
    )]
    public class SlotsInfoService : ISlotsInfo
    {
        private readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public GetSlotMachineJackpotResponse GetMachineJackpots(GetSlotMachineJackpotRequest reqst)
        {
            DateTime kickoff = DateTime.Now;
            XmlConfigurator.Configure(new System.IO.FileInfo(ConfigurationManager.AppSettings["Log4NetConfigPath"]));

            GetSlotMachineJackpotResponse reply = new GetSlotMachineJackpotResponse();
            try
            {
                log.Info("GetMachineJackpots Request. Casino: " + reqst.Casino + ". MachineNo: " + reqst.SlotMachine + ". NumberOfLines: " + reqst.NumberOfLines + ".");

                using (SlotsInfoServiceClient service = new SlotsInfoServiceClient(reqst.Casino + "BasicHttpBinding_SlotsInfoService"))
                {
                    svc.GetSlotMachineJackpotRequest request = new svc.GetSlotMachineJackpotRequest();
                    request.SlotMachine = reqst.SlotMachine;
                    request.NumberOfLines = reqst.NumberOfLines;

                    svc.GetSlotMachineJackpotResponse response = new svc.GetSlotMachineJackpotResponse();
                    response = service.GetMachineJackpots(request);

                    reply.Success = response.Success;
                    foreach (svc.JackpotDetail detail in response.Jackpots)
                        reply.Jackpots.Add(new JackpotDetail() { HitDate = detail.HitDate, Amount = detail.Amount, JPotType = detail.JPotType });
                    foreach (svc.ProcessError detail in response.Errors)
                        reply.Errors.Add(new ProcessError() { ErrorMessage = detail.ErrorMessage});
                }

                log.Info("Request fulfilled. (" + executionTime(new TimeSpan(DateTime.Now.Ticks - kickoff.Ticks)) + "ms)");

                reply.Success = true;
                return reply;
            }
            catch (Exception excp)
            {
                log.Error("\nMessage:\n" + excp.Message + "\nStack Trace:\n" + excp.StackTrace);

                reply.Success = false;

                ProcessError err = new ProcessError();
                err.ErrorMessage = excp.Message;

                reply.Errors.Add(err);
                return reply;
            }
        }
        public GetJackpotPayoutResponse GetJackpotPayouts(GetJackpotPayoutRequest reqst)
        {
            DateTime kickoff = DateTime.Now;
            XmlConfigurator.Configure(new System.IO.FileInfo(ConfigurationManager.AppSettings["Log4NetConfigPath"]));

            GetJackpotPayoutResponse reply = new GetJackpotPayoutResponse();
            try
            {
                log.Info("GetJackpotPayouts Request. Casino: " + reqst.Casino + ". Start: " + reqst.Start.ToString("yyyy-MM-dd HH:mm") + ". End:" + reqst.End.ToString("yyyy-MM-dd HH:mm") + ". NumberOfLines: " + reqst.NumberOfLines + ".");

                using (SlotsInfoServiceClient service = new SlotsInfoServiceClient(reqst.Casino + "BasicHttpBinding_SlotsInfoService"))
                {
                    svc.GetJackpotPayoutRequest request = new svc.GetJackpotPayoutRequest();
                    request.Start = reqst.Start;
                    request.End = reqst.End;
                    request.NumberOfLines = reqst.NumberOfLines;

                    svc.GetJackpotPayoutResponse response = new svc.GetJackpotPayoutResponse();
                    response = service.GetJackpotPayouts(request);

                    reply.Success = response.Success;
                    reply.JackpotTotal = response.JackpotTotal;
                    reply.ProgressiveTotal = response.ProgressiveTotal;
                    reply.CelebrationTotal = response.CelebrationTotal;
                    reply.MysteryTotal = response.MysteryTotal;
                    reply.MachineOutMovement = response.MachineOutMovement;

                    foreach (svc.JackpotDetail detail in response.Jackpots)
                        reply.Jackpots.Add(new JackpotHitDetail()
                        {
                            HitDate = detail.HitDate,
                            Amount = detail.Amount,
                            JPotType = detail.JPotType,
                            SlotMachine = detail.SlotMachine,
                            Description = detail.Description
                        });
                    foreach (svc.ProcessError detail in response.Errors)
                        reply.Errors.Add(new ProcessError() { ErrorMessage = detail.ErrorMessage });
                }

                log.Info("Request fulfilled. (" + executionTime(new TimeSpan(DateTime.Now.Ticks - kickoff.Ticks)) + "ms)");

                reply.Success = true;
                return reply;
            }
            catch (Exception excp)
            {
                log.Error("\nMessage:\n" + excp.Message + "\nStack Trace:\n" + excp.StackTrace);

                reply.Success = false;

                ProcessError err = new ProcessError();
                err.ErrorMessage = excp.Message;

                reply.Errors.Add(err);
                return reply;
            }
        }
        public GetPointsBalanceResponse GetPointsBalances(GetPointsBalancesRequest reqst)
        {
            DateTime kickoff = DateTime.Now;
            XmlConfigurator.Configure(new System.IO.FileInfo(ConfigurationManager.AppSettings["Log4NetConfigPath"]));

            GetPointsBalanceResponse reply = new GetPointsBalanceResponse();
            try
            {
                log.Info("GetPointsBalances Request. AcctNo: " + reqst.AcctNo + ".");

                reply.Success = true;
                GetPointsBalancesFromDB(ref reply, reqst.AcctNo);

                log.Info("Request fulfilled. (" + executionTime(new TimeSpan(DateTime.Now.Ticks - kickoff.Ticks)) + "ms)");

                reply.Success = true;
                return reply;
            }
            catch (Exception excp)
            {
                log.Error("\nMessage:\n" + excp.Message + "\nStack Trace:\n" + excp.StackTrace);

                reply.Success = false;

                ProcessError err = new ProcessError();
                err.ErrorMessage = excp.Message;

                reply.Errors.Add(err);
                return reply;
            }
        }
        public GetPointsTranansactionResponse GetPointsTrans(GetPointsTransactionRequest reqst)
        {
            DateTime kickoff = DateTime.Now;
            XmlConfigurator.Configure(new System.IO.FileInfo(ConfigurationManager.AppSettings["Log4NetConfigPath"]));

            GetPointsTranansactionResponse reply = new GetPointsTranansactionResponse();
            try
            {
                log.Info("GetPointsTrans Request. Account Number: " + reqst.AcctNo + ". QueryDate: " + reqst.QueryDate.ToString("yyyy-MM-dd") + ". CasinoCode: " + reqst.CasinoCode);

                reply.Success = false;

                //lets call down to the unit
                log.Info("Calling remote property: " + reqst.CasinoCode + ".");
                using (SlotsInfoServiceClient service = new SlotsInfoServiceClient(reqst.CasinoCode + "BasicHttpBinding_SlotsInfoService"))
                {
                    svc.GetPointsTranRequest request = new svc.GetPointsTranRequest
                        {
                            AcctNo = reqst.AcctNo,
                            QueryDate = reqst.QueryDate,
                            CasinoCode = reqst.CasinoCode,
                            ZeroDate = reqst.ZeroDate
                        };

                    svc.GetPointsTranResponse response = new svc.GetPointsTranResponse();
                    response = service.GetPointsTransaction(request);

                    reply.Success = response.Success;
                    reply.PointsTran = new PointsTransaction
                    {
                        Date = response.PointsTransaction.Date,
                        Property = response.PointsTransaction.Property,

                        //casino
                        EarnedCasino = response.PointsTransaction.EarnedCasino,
                        BonusCasino = response.PointsTransaction.BonusCasino,
                        AdjPosCasino = response.PointsTransaction.AdjPosCasino,
                        RedeemCasino = response.PointsTransaction.RedeemCasino,
                        AdjNegCasino = response.PointsTransaction.AdjNegCasino,
                        ExpireCasino = response.PointsTransaction.ExpireCasino,

                        //leisure
                        EarnedLeisure = response.PointsTransaction.EarnedLeisure,
                        AdjPosLeisure = response.PointsTransaction.AdjPosLeisure,
                        RedeemLeisure = response.PointsTransaction.RedeemLeisure,
                        AdjNegLeisure = response.PointsTransaction.AdjNegLeisure,
                        ExpireLeisure = response.PointsTransaction.ExpireLeisure,

                        //tier credits
                        TierCredits = response.PointsTransaction.TierCredits
                    };

                    foreach (svc.ProcessError detail in response.Errors)
                        reply.Errors.Add(new ProcessError() { ErrorMessage = detail.ErrorMessage });
                }

                log.Info("Request fulfilled. (" + executionTime(new TimeSpan(DateTime.Now.Ticks - kickoff.Ticks)) + "ms)");

                return reply;
            }
            catch (Exception excp)
            {
                log.Error("\nMessage:\n" + excp.Message + "\nStack Trace:\n" + excp.StackTrace);

                reply.Success = false;

                ProcessError err = new ProcessError();
                err.ErrorMessage = excp.Message;

                reply.Errors.Add(err);
                return reply;
            }
        }

        public GetWapValueResponse GetWapVal(string WapId)
        {
            DateTime kickoff = DateTime.Now;
            XmlConfigurator.Configure(new System.IO.FileInfo(ConfigurationManager.AppSettings["Log4NetConfigPath"]));

            GetWapValueResponse reply = new GetWapValueResponse();
            reply.Success = false;

            string ln = "";

            try
            {
                log.Info("GetWapValues Request. WapId: " + WapId + ".");

                string url = config.WapQueryUrl + "onlinejackpotvalues?jpId=" + WapId + "&format=json";

                System.Net.WebRequest webRequest = System.Net.WebRequest.Create(url);
                webRequest.Method = "GET";

                System.IO.Stream objStream = webRequest.GetResponse().GetResponseStream();
                System.IO.StreamReader objReader = new System.IO.StreamReader(objStream);
                ln = objReader.ReadLine();

                onlineJackpotValuesResponse results = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<onlineJackpotValuesResponse>(ln);
                foreach (onlineJackpotValuesResponseResultonlineJackpotValues jackpotValue in results.result)
                {
                    if (jackpotValue.id == WapId)
                    {
                        reply.Detail = new WapValue();

                        reply.Detail.Id = jackpotValue.id;
                        reply.Detail.Name = jackpotValue.name;
                        reply.Detail.Value = jackpotValue.value;
                        reply.Detail.Error = jackpotValue.error;

                        reply.Success = true;
                        break;
                    }
                }
                if (reply.Detail == null)
                    reply.Errors.Add(new ProcessError { ErrorMessage = "WapId not found" });
                else
                    log.Info("Request fulfilled. (" + executionTime(new TimeSpan(DateTime.Now.Ticks - kickoff.Ticks)) + "ms)");
            }
            catch (Exception excp)
            {
                log.Error("\nMessage:\n" + excp.Message + "\nline:\n" + ln + "\nresponse:\n:" + ln + "\nStack Trace:\n" + excp.StackTrace);

                reply.Success = false;

                ProcessError err = new ProcessError();
                err.ErrorMessage = excp.Message;

                reply.Errors.Add(err);
                return reply;
            }
            return reply;
        }
        public GetWapHitResponse GetWapHits(GetWapHitsRequest reqst)
        {
            DateTime kickoff = DateTime.Now;
            XmlConfigurator.Configure(new System.IO.FileInfo(ConfigurationManager.AppSettings["Log4NetConfigPath"]));

            GetWapHitResponse reply = new GetWapHitResponse();
            reply.Success = false;

            string ln = "";

            try
            {
                log.Info("GetWapHits Request. WapId: " + reqst.WapId + ". NumberOfLines: " + reqst.NumberOfLines + ".");

                string url = config.WapQueryUrl + "jackpothits?jpId=" + reqst.WapId + "&noOfRecords=" + reqst.NumberOfLines + "&orderedBy=time&format=json";

                System.Net.WebRequest webRequest = System.Net.WebRequest.Create(url);
                webRequest.Method = "GET";

                System.IO.Stream objStream = webRequest.GetResponse().GetResponseStream();
                System.IO.StreamReader objReader = new System.IO.StreamReader(objStream);
                ln = objReader.ReadLine();

                jackpotHitsResponse results = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<jackpotHitsResponse>(ln);

                reply.Success = true;

                reply.CalcTime = results.calcTime.ToString();
                reply.IsCached = results.isCached;
                reply.IsRunning = results.isRunning;

                if (results.oldResult != null)
                    foreach (jackpotHitsResponseResultjackpotHits hit in results.oldResult)
                    {
                        reply.OldResults.Add(new WapJackpotHits
                        {
                            Amount = hit.amount,
                            CasinoGroup = hit.casinoGroup,
                            CasinoName = hit.casinoName,
                            GamingDay = hit.gamingDay,
                            Info1 = hit.info1,
                            Info2 = hit.info2,
                            Instance = hit.instance,
                            InventoryNr = hit.inventoryNr,
                            Jackpot = hit.jackpot,
                            JpId = hit.jpId,
                            MainAmount = hit.mainAmount,
                            PaidAmount = hit.paidAmount,
                            ParticipatingEgds = hit.participatingEgds,
                            ParticipationEgdAmount = hit.participationEgdAmount,
                            Position = hit.position,
                            PositionShort = hit.positionShort,
                            Time = hit.time,
                            TotalParticipationAmount = hit.totalParticipationAmount
                        });
                    }
                if (results.result != null)
                    foreach (jackpotHitsResponseResultjackpotHits hit in results.result)
                    {
                        reply.Results.Add(new WapJackpotHits
                        {
                            Amount = hit.amount,
                            CasinoGroup = hit.casinoGroup,
                            CasinoName = hit.casinoName,
                            GamingDay = hit.gamingDay,
                            Info1 = hit.info1,
                            Info2 = hit.info2,
                            Instance = hit.instance,
                            InventoryNr = hit.inventoryNr,
                            Jackpot = hit.jackpot,
                            JpId = hit.jpId,
                            MainAmount = hit.mainAmount,
                            PaidAmount = hit.paidAmount,
                            ParticipatingEgds = hit.participatingEgds,
                            ParticipationEgdAmount = hit.participationEgdAmount,
                            Position = hit.position,
                            PositionShort = hit.positionShort,
                            Time = hit.time,
                            TotalParticipationAmount = hit.totalParticipationAmount
                        });
                    }
                if ((reply.OldResults == null || reply.OldResults.Count < 1) && (reply.Results == null || reply.Results.Count < 1))
                {
                    reply.Success = true;
                    reply.Errors.Add(new ProcessError { ErrorMessage = "WapId not found" });
                }
                else
                    log.Info("Request fulfilled. (" + executionTime(new TimeSpan(DateTime.Now.Ticks - kickoff.Ticks)) + "ms)");

            }
            catch (Exception excp)
            {
                log.Error("\nMessage:\n" + excp.Message + "\nresponse:\n" + ln + "\nStack Trace:\n" + excp.StackTrace);

                reply.Success = false;

                ProcessError err = new ProcessError();
                err.ErrorMessage = excp.Message;

                reply.Errors.Add(err);
                return reply;
            }
            return reply;
        }

        public GetGameCelebrationsResponse GetGameCelebrationsByValue(GetGameCelebrationsValueRequest reqst)
        {
            DateTime kickoff = DateTime.Now;
            XmlConfigurator.Configure(new System.IO.FileInfo(config.Log4NetConfigPath));

            GetGameCelebrationsResponse reply = new GetGameCelebrationsResponse();
            reply.Payouts = new GameCelebrationsList();

            try
            {
                log.Info("GetGameCelebrationsAbove(" + reqst.Amount + ") for " + reqst.Casino + " Request.");

                using (SlotsInfoServiceClient service = new SlotsInfoServiceClient(reqst.Casino + "BasicHttpBinding_SlotsInfoService"))
                {
                    svc.GetGameCelebrationsValueRequest request = new svc.GetGameCelebrationsValueRequest();
                    request.Amount = reqst.Amount;
                    request.NumberOfLines = reqst.NumberOfLines;

                    svc.GetGameCelebrationsResponse response = new svc.GetGameCelebrationsResponse();
                    response = service.GetGameCelebrationsByValue(request);

                    reply.Success = response.Success;
                    if (response.Payouts != null)
                    {
                        foreach (svc.GameCelebrationsDetail detail in response.Payouts)
                            reply.Payouts.Add(new GameCelebrationsHits
                            {
                                Amount = detail.Amount,
                                SlotMachine = detail.SlotMachine,
                                HitDate = detail.HitDate
                            });
                    }
                    foreach (svc.ProcessError detail in response.Errors)
                        reply.Errors.Add(new ProcessError() { ErrorMessage = detail.ErrorMessage });
                }

                log.Info("Request fulfilled. (" + executionTime(new TimeSpan(DateTime.Now.Ticks - kickoff.Ticks)) + "ms)");

                reply.Success = true;
                return reply;
            }
            catch (Exception excp)
            {
                log.Error("\nMessage:\n" + excp.Message + "\nStack Trace:\n" + excp.StackTrace);

                reply.Success = false;

                ProcessError err = new ProcessError();
                err.ErrorMessage = excp.Message;

                reply.Errors.Add(err);
                return reply;
            }
        }
        public GetGameCelebrationsResponse GetGameCelebrationsByMachine(GetGameCelebrationsMachineRequest reqst)
        {
            DateTime kickoff = DateTime.Now;
            XmlConfigurator.Configure(new System.IO.FileInfo(config.Log4NetConfigPath));

            GetGameCelebrationsResponse reply = new GetGameCelebrationsResponse();
            reply.Payouts = new GameCelebrationsList();

            try
            {
                log.Info("GetGameCelebrationsByMachine(" + reqst.MachineNo + ")Above(" + reqst.Amount + ") Request.");

                using (SlotsInfoServiceClient service = new SlotsInfoServiceClient(reqst.Casino + "BasicHttpBinding_SlotsInfoService"))
                {
                    svc.GetGameCelebrationsMachineRequest request = new svc.GetGameCelebrationsMachineRequest();
                    request.MachineNo = reqst.MachineNo;
                    request.Amount = reqst.Amount;
                    request.NumberOfLines = reqst.NumberOfLines;

                    svc.GetGameCelebrationsResponse response = new svc.GetGameCelebrationsResponse();
                    response = service.GetGameCelebrationsByMachine(request);

                    reply.Success = response.Success;
                    if (response.Payouts != null)
                    {
                        foreach (svc.GameCelebrationsDetail detail in response.Payouts)
                            reply.Payouts.Add(new GameCelebrationsHits
                            {
                                Amount = detail.Amount,
                                SlotMachine = detail.SlotMachine,
                                HitDate = detail.HitDate
                            });
                    }
                    foreach (svc.ProcessError detail in response.Errors)
                        reply.Errors.Add(new ProcessError() { ErrorMessage = detail.ErrorMessage });
                }

                log.Info("Request fulfilled. (" + executionTime(new TimeSpan(DateTime.Now.Ticks - kickoff.Ticks)) + "ms)");

                reply.Success = true;
                return reply;
            }
            catch (Exception excp)
            {
                log.Error("\nMessage:\n" + excp.Message + "\nStack Trace:\n" + excp.StackTrace);

                reply.Success = false;

                ProcessError err = new ProcessError();
                err.ErrorMessage = excp.Message;

                reply.Errors.Add(err);
                return reply;
            }
        }
        public GetMVGOffersResponse GetOffers(GetMVGOffersRequest reqst)
        {
            DateTime kickoff = DateTime.Now;
            XmlConfigurator.Configure(new System.IO.FileInfo(config.Log4NetConfigPath));

            GetMVGOffersResponse reply = new GetMVGOffersResponse();
            reply.Offers = new OfferList();

            try
            {
                log.Info("GetOffers. AcctNo: " + reqst.AcctNo + ", Start: " + reqst.Start.ToString("yyyy-MM-dd") + ", End: " + reqst.End.ToString("yyyy-MM-dd") + ".");

                reply.Success = true;
                GetOffersFromDB(ref reply, reqst);

                log.Info("Request fulfilled. (" + executionTime(new TimeSpan(DateTime.Now.Ticks - kickoff.Ticks)) + "ms)");

                reply.Success = true;
                return reply;
            }
            catch (Exception excp)
            {
                log.Error("\nMessage:\n" + excp.Message + "\nStack Trace:\n" + excp.StackTrace);

                reply.Success = false;

                ProcessError err = new ProcessError();
                err.ErrorMessage = excp.Message;

                reply.Errors.Add(err);
                return reply;
            }
        }

        private void GetPointsBalancesFromDB(ref GetPointsBalanceResponse reply, String acctNo)
        {
            reply.Success = false;

            using (SqlConnection sqlConnection = new SqlConnection("user id=" + config.Cmp_User + ";" +
                "password=" + config.Cmp_Password + ";" +
                "server=" + config.Cmp_Server + ";" +
                "Trusted_Connection=false;" +
                "database=" + config.Cmp_Database + "; " +
                "connection timeout=120"))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(GetPointsBalancesQueryString(acctNo), sqlConnection))
                {
                    sqlCommand.CommandTimeout = config.SqlTimeOut;
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            string checkedOutCasino = DBHelper.dbHelper.getDbStringVal(dataReader, "CasinoCode");
                            if (checkedOutCasino == "")
                            {
                                reply.CasinPoints = DBHelper.dbHelper.getDbIntVal(dataReader, "PointsBal");
                                reply.LeisurePoints = DBHelper.dbHelper.getDbIntVal(dataReader, "CompBal");
                            }
                            else
                            {
                                reply.CheckedOutCasino = checkedOutCasino;
                                //lets call down to the unit
                                log.Info("Calling remote property: " + checkedOutCasino + ".");
                                using (SlotsInfoServiceClient service = new SlotsInfoServiceClient(checkedOutCasino + "BasicHttpBinding_SlotsInfoService"))
                                {
                                    svc.GetPointsBalancesRequest request = new svc.GetPointsBalancesRequest();
                                    request.AcctNo = acctNo;

                                    svc.GetPointsBalanceResponse response = new svc.GetPointsBalanceResponse();
                                    response = service.GetPointsBalances(request);

                                    reply.Success = response.Success;
                                    reply.CasinPoints = response.CasinPoints;
                                    reply.LeisurePoints = response.LeisurePoints;

                                    foreach (svc.ProcessError detail in response.Errors)
                                        reply.Errors.Add(new ProcessError() { ErrorMessage = detail.ErrorMessage });
                                }
                            }
                        }

                        reply.Success = true;
                    }
                }
            }
        }
        private void GetOffersFromDB(ref GetMVGOffersResponse reply, GetMVGOffersRequest reqst)
        {
            reply.Success = false;

            using (SqlConnection sqlConnection = new SqlConnection("user id=" + config.DW_User + ";" +
                "password=" + config.DW_Password + ";" +
                "server=" + config.DW_Server + ";" +
                "Trusted_Connection=false;" +
                "database=" + config.DW_Database + "; " +
                "connection timeout=120"))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(GetOffersQueryString(reqst), sqlConnection))
                {
                    sqlCommand.CommandTimeout = config.SqlTimeOut;
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        reply.Offers = new OfferList();
                        while (dataReader.Read())
                        {
                            reply.Offers.Add(
                                new MVGOffer
                                {
                                    PlayerAccountNumber = DBHelper.dbHelper.getDbStringVal(dataReader, "PlayerAccountNumber"),
                                    MktgEventName = DBHelper.dbHelper.getDbStringVal(dataReader, "MktgEventName"),
                                    EventStartDtm = DBHelper.dbHelper.getDbDateVal(dataReader, "EventStartDtm"),
                                    EventEndDtm = DBHelper.dbHelper.getDbDateVal(dataReader, "EventEndDtm"),
                                    OfferDescName = DBHelper.dbHelper.getDbStringVal(dataReader, "OfferDescName"),
                                    OfferDetailedDesc = DBHelper.dbHelper.getDbStringVal(dataReader, "OfferDetailedDesc"),
                                    OfferCategoryName = DBHelper.dbHelper.getDbStringVal(dataReader, "OfferCategoryName"),
                                    RedeemedDate = DBHelper.dbHelper.getDbDateVal(dataReader, "RedeemedDate"),
                                    OfferCost = (double)DBHelper.dbHelper.getDbDecimalNullVal(dataReader, "OfferCost"),
                                    IsRedeemed = DBHelper.dbHelper.getDbBoolVal(dataReader, "IsRedeemed"),
                                    IsExpired = DBHelper.dbHelper.getDbBoolVal(dataReader, "IsExpired"),
                                    CasinoId = DBHelper.dbHelper.getDbStringVal(dataReader, "CasinoId"),
                                    OfferValidEffectiveDate = DBHelper.dbHelper.getDbDateVal(dataReader, "OfferValidEffectiveDate"),
                                    OfferValidThruDate = DBHelper.dbHelper.getDbDateVal(dataReader, "OfferValidThruDate")
                                }
                            );
                        }

                        reply.Success = true;
                    }
                }
            }
        }

        private String GetPointsBalancesQueryString(string acctNo)
        {
            return
"declare @Acct nvarchar(10) = '" + acctNo + "'\n\n" +
"select\n" +
"  isnull(ptsbal.PtsBal, 0) as PointsBal,\n" +
"  isnull(cast(floor(compbal.EarnedComp-compbal.CompUsed-compbal.AdjCompDr+compbal.AdjCompCr-compbal.ExpireComp) as int), 0) as CompBal,\n" +
"  isnull(cas.CasinoCode, '') as CasinoCode\n" +
"  from tPlayerPointBal (nolock) as ptsbal\n" +
"  left outer join tPlayerCompBal (nolock) as compbal on compbal.PlayerId = ptsbal.PlayerId\n" +
"  inner join tPlayerCard (nolock) as c on c.PlayerId = ptsbal.PlayerId\n" +
"  left join tActiveplayeraccount (nolock) as a on a.PlayerId = ptsbal.PlayerId\n" +
"  left join tCasino as Cas (nolock) on cas.SiteId = a.SiteId\n" +
"  where c.Acct = @Acct";
        }
        private String GetOffersQueryString(GetMVGOffersRequest reqst)
        {
            return @"
declare @acctNo nvarchar(10) = '" + reqst.AcctNo + @"'
declare @startDate DateTime = '" + reqst.Start.ToString("yyyy-MM-dd") + @"'
declare @endDate DateTime  = '" + reqst.End.ToString("yyyy-MM-dd") + @"'

select
  PlayerAccountNumber,
  MktgEventName,
  EventStartDtm,
  EventEndDtm,
  OfferDescName,
  OfferDetailedDesc,
  OfferCategoryName,
  RedeemedDate,
  OfferCost,
  IsRedeemed,
  IsExpired,
  CasinoId,
  OfferValidEffectiveDate,
  OfferValidThruDate
from fBICM_CMMktgOffer 
where
  PlayerAccountNumber=@acctNo and
  EventStartDtm >= @startDate and
  EventStartDtm < @endDate
order by OfferValidThruDate asc";
        }

        private string executionTime(TimeSpan ts)
        {
            int diff = (int)(ts).TotalMilliseconds;
            string strDiff;
            if (diff < 1000) strDiff = diff.ToString("0");
            else if (diff < 1000000) strDiff = diff.ToString("# ##0");
            else strDiff = diff.ToString("# ### ##0");
            return strDiff;
        }
    }
    public static class config
    {
        private static int sqlTimeOut;

        private static string cmp_Server;
        private static string cmp_Database;
        private static string cmp_User;
        private static string cmp_Password;

        private static string dw_Server;
        private static string dw_Database;
        private static string dw_User;
        private static string dw_Password;

        private static string wapQueryUrl;

        private static string log4NetConfigPath;

        public static int SqlTimeOut
        {
            get
            {
                if (sqlTimeOut == 0)
                {
                    int ret = 120;
                    try { ret = int.Parse(ConfigurationManager.AppSettings["SQLTimeOut"]); } catch { }
                    sqlTimeOut = ret;
                }
                return sqlTimeOut;
            }
        }

        public static string Cmp_Server
        {
            get
            {
                if (cmp_Server == null)
                {
                    string str = ConfigurationManager.AppSettings["CMP_Server"];
                    if (str == null || str.Trim() == "")
                        throw new Exception("Error in Config, CMP_Server not defined");
                    cmp_Server = str;
                }
                return cmp_Server;
            }
        }
        public static string Cmp_Database
        {
            get
            {
                if (cmp_Database == null)
                {
                    string str = ConfigurationManager.AppSettings["CMP_Database"];
                    if (str == null || str.Trim() == "")
                        throw new Exception("Error in Config, CMP_Database not defined");
                    cmp_Database = str;
                }
                return cmp_Database;
            }
        }
        public static string Cmp_User
        {
            get
            {
                if (cmp_User == null)
                {
                    string str = ConfigurationManager.AppSettings["CMP_User"];
                    if (str == null || str.Trim() == "")
                        str = "BallySA";
                    cmp_User = str;
                }
                return cmp_User;
            }
        }
        public static string Cmp_Password
        {
            get
            {
                if (cmp_Password == null)
                {
                    string str = ConfigurationManager.AppSettings["CMP_Password"];
                    if (str == null || str.Trim() == "")
                        str = "Ba!!ySA@123";
                    cmp_Password = str;
                }
                return cmp_Password;
            }
        }

        public static string DW_Server
        {
            get
            {
                if (dw_Server == null)
                {
                    string str = ConfigurationManager.AppSettings["DW_Server"];
                    if (str == null || str.Trim() == "")
                        str = "10.9.240.78,17001";
                    dw_Server = str;
                }
                return dw_Server;
            }
        }
        public static string DW_Database
        {
            get
            {
                if (dw_Database == null)
                {
                    string str = ConfigurationManager.AppSettings["DW_Database"];
                    if (str == null || str.Trim() == "")
                        str = "DW_SA";
                    dw_Database = str;
                }
                return dw_Database;
            }
        }
        public static string DW_User
        {
            get
            {
                if (dw_User == null)
                {
                    string str = ConfigurationManager.AppSettings["DW_User"];
                    if (str == null || str.Trim() == "")
                        str = "SSIS_Execute";
                    dw_User = str;
                }
                return dw_User;
            }
        }
        public static string DW_Password
        {
            get
            {
                if (dw_Password == null)
                {
                    string str = ConfigurationManager.AppSettings["DW_Password"];
                    if (str == null || str.Trim() == "")
                        str = "VBNfghrty@567";
                    dw_Password = str;
                }
                return dw_Password;
            }
        }
        public static string WapQueryUrl
        {
            get
            {
                if (wapQueryUrl == null)
                {
                    string str = ConfigurationManager.AppSettings["wapQueryUrl"];
                    if (str == null || str.Trim() == "")
                        str = "http://10.9.13.33:2012/api/custom/casino/table/";
                    wapQueryUrl = str;
                }
                return wapQueryUrl;
            }
        }

        public static String Log4NetConfigPath
        {
            get
            {
                if (log4NetConfigPath == null)
                    log4NetConfigPath = ConfigurationManager.AppSettings["Log4NetConfigPath"];
                return log4NetConfigPath;
            }
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class onlineJackpotValuesResponse
{
    private int calcTimeField;

    private bool isCachedField;

    private bool isRunningField;

    private object oldResultField;

    private onlineJackpotValuesResponseResultonlineJackpotValues[] resultField;

    /// <remarks/>
    public int calcTime
    {
        get
        {
            return this.calcTimeField;
        }
        set
        {
            this.calcTimeField = value;
        }
    }

    /// <remarks/>
    public bool isCached
    {
        get
        {
            return this.isCachedField;
        }
        set
        {
            this.isCachedField = value;
        }
    }

    /// <remarks/>
    public bool isRunning
    {
        get
        {
            return this.isRunningField;
        }
        set
        {
            this.isRunningField = value;
        }
    }

    /// <remarks/>
    public object oldResult
    {
        get
        {
            return this.oldResultField;
        }
        set
        {
            this.oldResultField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("resultonlineJackpotValues", IsNullable = true)]
    public onlineJackpotValuesResponseResultonlineJackpotValues[] result
    {
        get
        {
            return this.resultField;
        }
        set
        {
            this.resultField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class onlineJackpotValuesResponseResultonlineJackpotValues
{

    private string errorField;

    private string idField;

    private string nameField;

    private Int64 valueField;

    /// <remarks/>
    public string error
    {
        get
        {
            return this.errorField;
        }
        set
        {
            this.errorField = value;
        }
    }

    /// <remarks/>
    public string id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <remarks/>
    public Int64 value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}


/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class jackpotHitsResponse
{
    private int calcTimeField;

    private bool isCachedField;

    private bool isRunningField;

    private jackpotHitsResponseResultjackpotHits[] oldResultField;

    private jackpotHitsResponseResultjackpotHits[] resultField;

    /// <remarks/>
    public int calcTime
    {
        get
        {
            return this.calcTimeField;
        }
        set
        {
            this.calcTimeField = value;
        }
    }

    /// <remarks/>
    public bool isCached
    {
        get
        {
            return this.isCachedField;
        }
        set
        {
            this.isCachedField = value;
        }
    }

    /// <remarks/>
    public bool isRunning
    {
        get
        {
            return this.isRunningField;
        }
        set
        {
            this.isRunningField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("resultjackpotHits", IsNullable = true)]
    public jackpotHitsResponseResultjackpotHits[] oldResult
    {
        get
        {
            return this.oldResultField;
        }
        set
        {
            this.oldResultField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("resultjackpotHits", IsNullable = true)]
    public jackpotHitsResponseResultjackpotHits[] result
    {
        get
        {
            return this.resultField;
        }
        set
        {
            this.resultField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class jackpotHitsResponseResultjackpotHits
{
    private Int64 amountField;

    private string casinoGroupField;

    private string casinoNameField;

    private System.DateTime gamingDayField;

    private string info1Field;

    private string info2Field;

    private Int64 instanceField;

    private string inventoryNrField;

    private string jackpotField;

    private string jpIdField;

    private Int64 mainAmountField;

    private Int64 paidAmountField;

    private Int64 participatingEgdsField;

    private Int64 participationEgdAmountField;

    private string positionField;

    private string positionShortField;

    private System.DateTime timeField;

    private Int64 totalParticipationAmountField;

    /// <remarks/>
    public Int64 amount
    {
        get
        {
            return this.amountField;
        }
        set
        {
            this.amountField = value;
        }
    }

    /// <remarks/>
    public string casinoGroup
    {
        get
        {
            return this.casinoGroupField;
        }
        set
        {
            this.casinoGroupField = value;
        }
    }

    /// <remarks/>
    public string casinoName
    {
        get
        {
            return this.casinoNameField;
        }
        set
        {
            this.casinoNameField = value;
        }
    }

    /// <remarks/>
    public System.DateTime gamingDay
    {
        get
        {
            return this.gamingDayField;
        }
        set
        {
            this.gamingDayField = value;
        }
    }

    /// <remarks/>
    public string info1
    {
        get
        {
            return this.info1Field;
        }
        set
        {
            this.info1Field = value;
        }
    }

    /// <remarks/>
    public string info2
    {
        get
        {
            return this.info2Field;
        }
        set
        {
            this.info2Field = value;
        }
    }

    /// <remarks/>
    public Int64 instance
    {
        get
        {
            return this.instanceField;
        }
        set
        {
            this.instanceField = value;
        }
    }

    /// <remarks/>
    public string inventoryNr
    {
        get
        {
            return this.inventoryNrField;
        }
        set
        {
            this.inventoryNrField = value;
        }
    }

    /// <remarks/>
    public string jackpot
    {
        get
        {
            return this.jackpotField;
        }
        set
        {
            this.jackpotField = value;
        }
    }

    /// <remarks/>
    public string jpId
    {
        get
        {
            return this.jpIdField;
        }
        set
        {
            this.jpIdField = value;
        }
    }

    /// <remarks/>
    public Int64 mainAmount
    {
        get
        {
            return this.mainAmountField;
        }
        set
        {
            this.mainAmountField = value;
        }
    }

    /// <remarks/>
    public Int64 paidAmount
    {
        get
        {
            return this.paidAmountField;
        }
        set
        {
            this.paidAmountField = value;
        }
    }

    /// <remarks/>
    public Int64 participatingEgds
    {
        get
        {
            return this.participatingEgdsField;
        }
        set
        {
            this.participatingEgdsField = value;
        }
    }

    /// <remarks/>
    public Int64 participationEgdAmount
    {
        get
        {
            return this.participationEgdAmountField;
        }
        set
        {
            this.participationEgdAmountField = value;
        }
    }

    /// <remarks/>
    public string position
    {
        get
        {
            return this.positionField;
        }
        set
        {
            this.positionField = value;
        }
    }

    /// <remarks/>
    public string positionShort
    {
        get
        {
            return this.positionShortField;
        }
        set
        {
            this.positionShortField = value;
        }
    }

    /// <remarks/>
    public System.DateTime time
    {
        get
        {
            return this.timeField;
        }
        set
        {
            this.timeField = value;
        }
    }

    /// <remarks/>
    public Int64 totalParticipationAmount
    {
        get
        {
            return this.totalParticipationAmountField;
        }
        set
        {
            this.totalParticipationAmountField = value;
        }
    }
}
