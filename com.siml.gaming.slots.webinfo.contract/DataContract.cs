using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;
namespace com.siml.gaming.slots.webinfo.contract
{
    [DataContract(Namespace = "http://www.suninternational.com/schemas/SunIntSchemas/gaming/web/SlotsInfo/v1.01")]
    public class GetSlotMachineJackpotRequest
    {
        private string casino;
        private string slotMachine;
        private int numberOfLines;

        [DataMember(IsRequired = true, Order = 0)]
        public string Casino
        {
            get { return casino; }
            set { casino = value; }
        }

        [DataMember(IsRequired = true, Order = 1)]
        public String SlotMachine
        {
            get { return slotMachine; }
            set { slotMachine = value; }
        }

        [DataMember(IsRequired = true, Order = 2)]
        public int NumberOfLines
        {
            get { return numberOfLines; }
            set { numberOfLines = value; }
        }
    }

    [DataContract(Namespace = "http://www.suninternational.com/schemas/SunIntSchemas/gaming/web/SlotsInfo/v1.01")]
    public class GetSlotMachineJackpotResponse
    {
        private bool success;
        private JackpotList jackpots = new JackpotList();
        private ErrorList errors = new ErrorList();

        [DataMember(IsRequired = true, Order = 0)] //, EmitDefaultValue = false)]
        public bool Success
        {
            get { return success; }
            set { success = value; }
        }

        [DataMember(Order = 1)]
        public JackpotList Jackpots
        {
            get { return jackpots; }
            set { jackpots = value; }
        }

        [DataMember(Order = 2)]
        public ErrorList Errors
        {
            get { return errors; }
            set { errors = value; }
        }
    }

    [CollectionDataContract(Namespace = "http://www.suninternational.com/schemas/SunIntSchemas/gaming/web/SlotsInfo/v1.01", ItemName = "item")]
    public class JackpotList : List<JackpotDetail>
    {
        public JackpotList() : base() { ;}
        public JackpotList(IEnumerable<JackpotDetail> collection) : base(collection) { ;}
        public JackpotList(int capacity) : base(capacity) { ;}
    }

    [DataContract(Namespace = "http://www.suninternational.com/schemas/SunIntSchemas/gaming/web/SlotsInfo/v1.01")]
    public class JackpotDetail
    {
        private double amount;
        private DateTime hitDate;
        private string jpotType;

        [DataMember(IsRequired = true, Order = 0)]
        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        [DataMember(IsRequired = true, Order = 1)]
        public DateTime HitDate
        {
            get { return hitDate; }
            set { hitDate = value; }
        }

        [DataMember(IsRequired = true, Order = 2)]
        public string JPotType
        {
            get { return jpotType; }
            set { jpotType = value; }
        }
    }

    [DataContract(Namespace = "http://www.suninternational.com/schemas/SunIntSchemas/gaming/web/SlotsInfo/v1.01")]
    public class GetJackpotPayoutRequest
    {
        private string casino;
        private DateTime start;
        private DateTime end;
        private int numberOfLines;

        [DataMember(IsRequired = true, Order = 0)]
        public string Casino
        {
            get { return casino; }
            set { casino = value; }
        }

        [DataMember(IsRequired = true, Order = 1)]
        public DateTime Start
        {
            get { return start; }
            set { start = value; }
        }

        [DataMember(IsRequired = true, Order = 2)]
        public DateTime End
        {
            get { return end; }
            set { end = value; }
        }

        [DataMember(IsRequired = true, Order = 3)]
        public int NumberOfLines
        {
            get { return numberOfLines; }
            set { numberOfLines = value; }
        }
    }

    [DataContract(Namespace = "http://www.suninternational.com/schemas/SunIntSchemas/gaming/web/SlotsInfo/v1.01")]
    public class GetJackpotPayoutResponse
    {
        private bool success;
        private Int64 jackpotTotal;
        private Int64 progressiveTotal;
        private Int64 celebrationTotal;
        private Int64 mysteryTotal;
        private Int64 machineOutMovement;
        private JackpotHitList jackpots = new JackpotHitList();
        private ErrorList errors = new ErrorList();

        [DataMember(IsRequired = true, Order = 0)] //, EmitDefaultValue = false)]
        public bool Success
        {
            get { return success; }
            set { success = value; }
        }

        [DataMember(IsRequired = true, Order = 1)]
        public Int64 JackpotTotal
        {
            get { return jackpotTotal; }
            set { jackpotTotal = value; }
        }

        [DataMember(IsRequired = true, Order = 2)]
        public Int64 ProgressiveTotal
        {
            get { return progressiveTotal; }
            set { progressiveTotal = value; }
        }

        [DataMember(IsRequired = true, Order = 3)]
        public Int64 CelebrationTotal
        {
            get { return celebrationTotal; }
            set { celebrationTotal = value; }
        }

        [DataMember(IsRequired = true, Order = 4)]
        public Int64 MysteryTotal
        {
            get { return mysteryTotal; }
            set { mysteryTotal = value; }
        }

        [DataMember(IsRequired = false, Order = 5)]
        public Int64 MachineOutMovement
        {
            get { return machineOutMovement; }
            set { machineOutMovement = value; }
        }

        [DataMember(Order = 6)]
        public JackpotHitList Jackpots
        {
            get { return jackpots; }
            set { jackpots = value; }
        }

        [DataMember(Order = 7)]
        public ErrorList Errors
        {
            get { return errors; }
            set { errors = value; }
        }
    }

    [DataContract(Namespace = "http://www.suninternational.com/schemas/SunIntSchemas/gaming/web/SlotsInfo/v1.01")]
    public class GetPointsBalancesRequest
    {
        private String acctNo;

        [DataMember(IsRequired = true, Order = 0)]
        public String AcctNo
        {
            get { return acctNo; }
            set { acctNo = value; }
        }
    }

    [DataContract(Namespace = "http://www.suninternational.com/schemas/SunIntSchemas/gaming/web/SlotsInfo/v1.01")]
    public class GetPointsBalanceResponse
    {
        private bool success;
        private long casinPoints;
        private long leisurePoints;
        private string checkedOutCasino;
        private ErrorList errors = new ErrorList();

        [DataMember(IsRequired = true, Order = 0)]
        public bool Success
        {
            get { return success; }
            set { success = value; }
        }

        [DataMember(IsRequired = true, Order = 1)]
        public long CasinPoints
        {
            get { return casinPoints; }
            set { casinPoints = value; }
        }

        [DataMember(IsRequired = true, Order = 2)]
        public long LeisurePoints
        {
            get { return leisurePoints; }
            set { leisurePoints = value; }
        }

        [DataMember(IsRequired = true, Order = 3)]
        public String CheckedOutCasino
        {
            get { return checkedOutCasino; }
            set { checkedOutCasino = value; }
        }

        [DataMember(Order = 4)]
        public ErrorList Errors
        {
            get { return errors; }
            set { errors = value; }
        }
    }

    [DataContract(Namespace = "http://www.suninternational.com/schemas/SunIntSchemas/gaming/web/SlotsInfo/v1.01")]
    public class GetPointsTransactionRequest
    {
        private String acctNo;
        private DateTime dt;
        private string casinoCode;
        private DateTime zeroDate;

        [DataMember(IsRequired = true, Order = 0)]
        public String AcctNo
        {
            get { return acctNo; }
            set { acctNo = value; }
        }

        [DataMember(IsRequired = true, Order = 1)]
        public DateTime QueryDate
        {
            get { return dt; }
            set { dt = value; }
        }

        [DataMember(IsRequired = true, Order = 2)]
        public String CasinoCode
        {
            get { return casinoCode; }
            set { casinoCode = value; }
        }

        [DataMember(IsRequired = true, Order = 3)]
        public DateTime ZeroDate
        {
            get { return zeroDate; }
            set { zeroDate = value; }
        }
    }

    [DataContract(Namespace = "http://www.suninternational.com/schemas/SunIntSchemas/gaming/web/SlotsInfo/v1.01")]
    public class GetPointsTranansactionResponse
    {
        private bool success;
        private PointsTransaction pointsTran;
        private ErrorList errors = new ErrorList();

        [DataMember(IsRequired = true, Order = 0)]
        public bool Success
        {
            get { return success; }
            set { success = value; }
        }

        [DataMember(IsRequired = true, Order = 1)]
        public PointsTransaction PointsTran
        {
            get { return pointsTran; }
            set { pointsTran = value; }
        }

        [DataMember(Order = 2)]
        public ErrorList Errors
        {
            get { return errors; }
            set { errors = value; }
        }
    }

    [DataContract(Namespace = "http://www.suninternational.com/schemas/SunIntSchemas/gaming/web/SlotsInfo/v1.01")]
    public class WapValue
    {
        private string id;
        private string name;
        private Int64 val;
        private string error;

        [DataMember(IsRequired = true, Order = 0)]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember(IsRequired = true, Order = 1)]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember(IsRequired = true, Order = 2)]
        public Int64 Value
        {
            get { return val; }
            set { val = value; }
        }

        [DataMember(IsRequired = true, Order = 3)]
        public string Error
        {
            get { return error; }
            set { error = value; }
        }
    }

    [DataContract(Namespace = "http://www.suninternational.com/schemas/SunIntSchemas/gaming/web/SlotsInfo/v1.01")]
    public class GetWapValueResponse
    {
        private bool success;
        private WapValue detail;
        private ErrorList errors = new ErrorList();

        [DataMember(IsRequired = true, Order = 0)]
        public bool Success
        {
            get { return success; }
            set { success = value; }
        }

        [DataMember(IsRequired = true, Order = 1)]
        public WapValue Detail
        {
            get { return detail; }
            set { detail = value; }
        }

        [DataMember(Order = 2)]
        public ErrorList Errors
        {
            get { return errors; }
            set { errors = value; }
        }
    }

    [DataContract(Namespace = "http://www.suninternational.com/schemas/SunIntSchemas/gaming/web/SlotsInfo/v1.01")]
    public class GetWapHitsRequest
    {
        private string wapId;
        private int numberOfLines;

        [DataMember(IsRequired = true, Order = 0)]
        public String WapId
        {
            get { return wapId; }
            set { wapId = value; }
        }

        [DataMember(IsRequired = true, Order = 1)]
        public int NumberOfLines
        {
            get { return numberOfLines; }
            set { numberOfLines = value; }
        }
    }

    [DataContract(Namespace = "http://www.suninternational.com/schemas/SunIntSchemas/gaming/web/SlotsInfo/v1.01")]
    public class GetWapHitResponse
    {
        private bool success;

        //private byte calcTimeField;
        private string calcTimeField;
        private bool isCachedField;
        private bool isRunningField;
        private WapJackpotHitList oldResults = new WapJackpotHitList();
        private WapJackpotHitList results = new WapJackpotHitList();

        private ErrorList errors = new ErrorList();

        [DataMember(IsRequired = true, Order = 0)]
        public bool Success
        {
            get { return success; }
            set { success = value; }
        }

        [DataMember(IsRequired = true, Order = 1)]
        public string CalcTime
        {
            get { return calcTimeField; }
            set { calcTimeField = value; }
        }

        [DataMember(IsRequired = true, Order = 2)]
        public bool IsCached
        {
            get { return isCachedField; }
            set { isCachedField = value; }
        }

        [DataMember(IsRequired = true, Order = 3)]
        public bool IsRunning
        {
            get { return isRunningField; }
            set { isRunningField = value; }
        }

        [DataMember(Order = 4)]
        public WapJackpotHitList OldResults
        {
            get { return oldResults; }
            set { oldResults = value; }
        }

        [DataMember(Order = 5)]
        public WapJackpotHitList Results
        {
            get { return results; }
            set { results = value; }
        }
        
        [DataMember(Order = 6)]
        public ErrorList Errors
        {
            get { return errors; }
            set { errors = value; }
        }
    }

    [DataContract(Namespace = "http://www.suninternational.com/schemas/SunIntSchemas/gaming/web/SlotsInfo/v1.01")]
    public class GetGameCelebrationsValueRequest
    {
        private string casino;
        private Int64 amount;
        private Int64 numberOfLines;

        [DataMember(IsRequired = true, Order = 0)]
        public string Casino
        {
            get { return casino; }
            set { casino = value; }
        }

        [DataMember(IsRequired = true, Order = 1)]
        public Int64 Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        [DataMember(IsRequired = true, Order = 2)]
        public Int64 NumberOfLines
        {
            get { return numberOfLines; }
            set { numberOfLines = value; }
        }
    }

    [DataContract(Namespace = "http://www.suninternational.com/schemas/SunIntSchemas/gaming/web/SlotsInfo/v1.01")]
    public class GetGameCelebrationsMachineRequest
    {
        private string casino;
        private string machineNo;
        private Int64 amount;
        private Int64 numberOfLines;

        [DataMember(IsRequired = true, Order = 0)]
        public string Casino
        {
            get { return casino; }
            set { casino = value; }
        }

        [DataMember(IsRequired = true, Order = 1)]
        public string MachineNo
        {
            get { return machineNo; }
            set { machineNo = value; }
        }

        [DataMember(IsRequired = true, Order = 2)]
        public Int64 Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        [DataMember(IsRequired = true, Order = 3)]
        public Int64 NumberOfLines
        {
            get { return numberOfLines; }
            set { numberOfLines = value; }
        }
    }

    [DataContract(Namespace = "http://www.suninternational.com/schemas/SunIntSchemas/gaming/web/SlotsInfo/v1.01")]
    public class GetGameCelebrationsResponse
    {
        private bool success;
        private GameCelebrationsList payouts;
        private ErrorList errors = new ErrorList();

        [DataMember(IsRequired = true, Order = 0)]
        public bool Success
        {
            get { return success; }
            set { success = value; }
        }

        [DataMember(Order = 1)]
        public GameCelebrationsList Payouts
        {
            get { return payouts; }
            set { payouts = value; }
        }

        [DataMember(Order = 2)]
        public ErrorList Errors
        {
            get { return errors; }
            set { errors = value; }
        }
    }

    [DataContract(Namespace = "http://www.suninternational.com/schemas/SunIntSchemas/gaming/web/SlotsInfo/v1.01")]
    public class GetMVGOffersRequest
    {
        private string acctNo;
        private DateTime start;
        private DateTime end;

        [DataMember(IsRequired = true, Order = 0)]
        public string AcctNo
        {
            get { return acctNo; }
            set { acctNo = value; }
        }

        [DataMember(IsRequired = true, Order = 1)]
        public DateTime Start
        {
            get { return start; }
            set { start = value; }
        }

        [DataMember(IsRequired = true, Order = 2)]
        public DateTime End
        {
            get { return end; }
            set { end = value; }
        }
    }

    [DataContract(Namespace = "http://www.suninternational.com/schemas/SunIntSchemas/gaming/web/SlotsInfo/v1.01")]
    public class GetMVGOffersResponse
    {
        private bool success;
        private OfferList offers;
        private ErrorList errors = new ErrorList();

        [DataMember(IsRequired = true, Order = 0)]
        public bool Success
        {
            get { return success; }
            set { success = value; }
        }

        [DataMember(Order = 1)]
        public OfferList Offers
        {
            get { return offers; }
            set { offers = value; }
        }

        [DataMember(Order = 2)]
        public ErrorList Errors
        {
            get { return errors; }
            set { errors = value; }
        }
    }

    [CollectionDataContract(Namespace = "http://www.suninternational.com/schemas/SunIntSchemas/gaming/web/SlotsInfo/v1.01", ItemName = "item")]
    public class GameCelebrationsList : List<GameCelebrationsHits>
    {
        public GameCelebrationsList() : base() { ;}
        public GameCelebrationsList(IEnumerable<GameCelebrationsHits> collection) : base(collection) { ;}
        public GameCelebrationsList(int capacity) : base(capacity) { ;}
    }

    [DataContract(Namespace = "http://www.suninternational.com/schemas/SunIntSchemas/gaming/web/SlotsInfo/v1.01")]
    public class GameCelebrationsHits
    {
        private Int64 amount;
        private string slotMachine;
        private DateTime hitDate;

        [DataMember(IsRequired = true, Order = 0)]
        public Int64 Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        [DataMember(IsRequired = true, Order = 1)]
        public string SlotMachine
        {
            get { return slotMachine; }
            set { slotMachine = value; }
        }

        [DataMember(IsRequired = true, Order = 2)]
        public DateTime HitDate
        {
            get { return hitDate; }
            set { hitDate = value; }
        }
    }

    [CollectionDataContract(Namespace = "http://www.suninternational.com/schemas/SunIntSchemas/gaming/web/SlotsInfo/v1.01", ItemName = "item")]
    public class JackpotHitList : List<JackpotHitDetail>
    {
        public JackpotHitList() : base() { ;}
        public JackpotHitList(IEnumerable<JackpotHitDetail> collection) : base(collection) { ;}
        public JackpotHitList(int capacity) : base(capacity) { ;}
    }

    [DataContract(Namespace = "http://www.suninternational.com/schemas/SunIntSchemas/gaming/web/SlotsInfo/v1.01")]
    public class JackpotHitDetail
    {
        private double amount;
        private DateTime hitDate;
        private string jpotType;
        private string slotMachine;
        private string description;

        [DataMember(IsRequired = true, Order = 0)]
        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        [DataMember(IsRequired = true, Order = 1)]
        public DateTime HitDate
        {
            get { return hitDate; }
            set { hitDate = value; }
        }

        [DataMember(IsRequired = true, Order = 2)]
        public string JPotType
        {
            get { return jpotType; }
            set { jpotType = value; }
        }

        [DataMember(IsRequired = true, Order = 3)]
        public string SlotMachine
        {
            get { return slotMachine; }
            set { slotMachine = value; }
        }

        [DataMember(IsRequired = false, Order = 4)]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }

    [DataContract(Namespace = "http://www.suninternational.com/schemas/SunIntSchemas/gaming/web/SlotsInfo/v1.01")]
    public class PointsTransaction
    {
        private DateTime date;
        private string property;

        //casino
        private long earnedCasino;
        private long bonusCasino;
        private long adjPosCasino;
        private long redeemCasino;
        private long adjNegCasino;
        private long expireCasino;

        //leisure
        private long earnedLeisure;
        private long adjPosLeisure;
        private long redeemLeisure;
        private long adjNegLeisure;
        private long expireLeisure;

        //tier credits
        private long tierCredits;

        [DataMember(IsRequired = true, Order = 0)]
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        [DataMember(IsRequired = true, Order = 1)]
        public string Property
        {
            get { return property; }
            set { property = value; }
        }

        //casino
        [DataMember(IsRequired = true, Order = 2)]
        public long EarnedCasino
        {
            get { return earnedCasino; }
            set { earnedCasino = value; }
        }
        [DataMember(IsRequired = true, Order = 3)]
        public long BonusCasino
        {
            get { return bonusCasino; }
            set { bonusCasino = value; }
        }
        [DataMember(IsRequired = true, Order = 4)]
        public long AdjPosCasino
        {
            get { return adjPosCasino; }
            set { adjPosCasino = value; }
        }
        [DataMember(IsRequired = true, Order = 5)]
        public long RedeemCasino
        {
            get { return redeemCasino; }
            set { redeemCasino = value; }
        }
        [DataMember(IsRequired = true, Order = 6)]
        public long AdjNegCasino
        {
            get { return adjNegCasino; }
            set { adjNegCasino = value; }
        }
        [DataMember(IsRequired = true, Order = 7)]
        public long ExpireCasino
        {
            get { return expireCasino; }
            set { expireCasino = value; }
        }

        //leisure
        [DataMember(IsRequired = true, Order = 8)]
        public long EarnedLeisure
        {
            get { return earnedLeisure; }
            set { earnedLeisure = value; }
        }
        [DataMember(IsRequired = true, Order = 9)]
        public long AdjPosLeisure
        {
            get { return adjPosLeisure; }
            set { adjPosLeisure = value; }
        }
        [DataMember(IsRequired = true, Order = 10)]
        public long RedeemLeisure
        {
            get { return redeemLeisure; }
            set { redeemLeisure = value; }
        }
        [DataMember(IsRequired = true, Order = 11)]
        public long AdjNegLeisure
        {
            get { return adjNegLeisure; }
            set { adjNegLeisure = value; }
        }
        [DataMember(IsRequired = true, Order = 12)]
        public long ExpireLeisure
        {
            get { return expireLeisure; }
            set { expireLeisure = value; }
        }

        //tier credits
        [DataMember(IsRequired = true, Order = 13)]
        public long TierCredits
        {
            get { return tierCredits; }
            set { tierCredits = value; }
        }
    }

    [CollectionDataContract(Namespace = "http://www.suninternational.com/schemas/SunIntSchemas/gaming/web/SlotsInfo/v1.01", ItemName = "item")]
    public class WapJackpotHitList : List<WapJackpotHits>
    {
        public WapJackpotHitList() : base() { ;}
        public WapJackpotHitList(IEnumerable<WapJackpotHits> collection) : base(collection) { ;}
        public WapJackpotHitList(int capacity) : base(capacity) { ;}
    }

    [DataContract(Namespace = "http://www.suninternational.com/schemas/SunIntSchemas/gaming/web/SlotsInfo/v1.01")]
    public class WapJackpotHits
    {
        private Int64 amountField;
        private string casinoGroupField;
        private string casinoNameField;
        private DateTime gamingDayField;
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
        private DateTime timeField;
        private Int64 totalParticipationAmountField;

        [DataMember(IsRequired = true, Order = 0)]
        public Int64 Amount
        {
            get { return amountField; }
            set { amountField = value; }
        }

        [DataMember(IsRequired = true, Order = 1)]
        public string CasinoGroup
        {
            get { return casinoGroupField; }
            set { casinoGroupField = value; }
        }

        [DataMember(IsRequired = true, Order = 2)]
        public string CasinoName
        {
            get { return casinoNameField; }
            set { casinoNameField = value; }
        }

        [DataMember(IsRequired = true, Order = 3)]
        public DateTime GamingDay
        {
            get { return gamingDayField; }
            set { gamingDayField = value; }
        }

        [DataMember(IsRequired = true, Order = 4)]
        public string Info1
        {
            get { return info1Field; }
            set { info1Field = value; }
        }

        [DataMember(IsRequired = true, Order = 5)]
        public string Info2
        {
            get { return info2Field; }
            set { info2Field = value; }
        }

        [DataMember(IsRequired = true, Order = 6)]
        public Int64 Instance
        {
            get { return instanceField; }
            set { instanceField = value; }
        }

        [DataMember(IsRequired = true, Order = 7)]
        public string InventoryNr
        {
            get { return inventoryNrField; }
            set { inventoryNrField = value; }
        }

        [DataMember(IsRequired = true, Order = 8)]
        public string Jackpot
        {
            get { return jackpotField; }
            set { jackpotField = value; }
        }

        [DataMember(IsRequired = true, Order = 9)]
        public string JpId
        {
            get { return jpIdField; }
            set { jpIdField = value; }
        }

        [DataMember(IsRequired = true, Order = 10)]
        public Int64 MainAmount
        {
            get { return mainAmountField; }
            set { mainAmountField = value; }
        }

        [DataMember(IsRequired = true, Order = 11)]
        public Int64 PaidAmount
        {
            get { return paidAmountField; }
            set { paidAmountField = value; }
        }

        [DataMember(IsRequired = true, Order = 12)]
        public Int64 ParticipatingEgds
        {
            get { return participatingEgdsField; }
            set { participatingEgdsField = value; }
        }

        [DataMember(IsRequired = true, Order = 13)]
        public Int64 ParticipationEgdAmount
        {
            get { return participationEgdAmountField; }
            set { participationEgdAmountField = value; }
        }

        [DataMember(IsRequired = true, Order = 14)]
        public string Position
        {
            get { return positionField; }
            set { positionField = value; }
        }

        [DataMember(IsRequired = true, Order = 15)]
        public string PositionShort
        {
            get { return positionShortField; }
            set { positionShortField = value; }
        }

        [DataMember(IsRequired = true, Order = 16)]
        public DateTime Time
        {
            get { return timeField; }
            set { timeField = value; }
        }

        [DataMember(IsRequired = true, Order = 17)]
        public Int64 TotalParticipationAmount
        {
            get { return totalParticipationAmountField; }
            set { totalParticipationAmountField = value; }
        }
    }

    [CollectionDataContract(Namespace = "http://www.suninternational.com/schemas/SunIntSchemas/gaming/web/SlotsInfo/v1.01", ItemName = "item")]
    public class OfferList : List<MVGOffer>
    {
        public OfferList() : base() {; }
        public OfferList(IEnumerable<MVGOffer> collection) : base(collection) {; }
        public OfferList(int capacity) : base(capacity) {; }
    }

    [DataContract(Namespace = "http://www.suninternational.com/schemas/SunIntSchemas/gaming/web/SlotsInfo/v1.01")]
    public class MVGOffer
    {
        private string playerAccountNumber;
        private string mktgEventName;
        private DateTime? eventStartDtm;
        private DateTime? eventEndDtm;
        private string offerDescName;
        private string offerDetailedDesc;
        private string offerCategoryName;
        private DateTime? redeemedDate;
        private double offerCost;
        private bool isRedeemed;
        private bool isExpired;
        private string casinoId;
        private DateTime? offerValidEffectiveDate;
        private DateTime? offerValidThruDate;

        [DataMember(IsRequired = true, Order = 0)]
        public string PlayerAccountNumber
        {
            get { return playerAccountNumber; }
            set { playerAccountNumber = value; }
        }

        [DataMember(IsRequired = true, Order = 1)]
        public string MktgEventName
        {
            get { return mktgEventName; }
            set { mktgEventName = value; }
        }

        [DataMember(IsRequired = true, Order = 2)]
        public DateTime? EventStartDtm
        {
            get { return eventStartDtm; }
            set { eventStartDtm = value; }
        }

        [DataMember(IsRequired = true, Order = 3)]
        public DateTime? EventEndDtm
        {
            get { return eventEndDtm; }
            set { eventEndDtm = value; }
        }

        [DataMember(IsRequired = true, Order = 4)]
        public string OfferDescName
        {
            get { return offerDescName; }
            set { offerDescName = value; }
        }

        [DataMember(IsRequired = true, Order = 5)]
        public string OfferDetailedDesc
        {
            get { return offerDetailedDesc; }
            set { offerDetailedDesc = value; }
        }

        [DataMember(IsRequired = true, Order = 6)]
        public string OfferCategoryName
        {
            get { return offerCategoryName; }
            set { offerCategoryName = value; }
        }

        [DataMember(IsRequired = true, Order = 7)]
        public DateTime? RedeemedDate
        {
            get { return redeemedDate; }
            set { redeemedDate = value; }
        }

        [DataMember(IsRequired = true, Order = 8)]
        public double OfferCost
        {
            get { return offerCost; }
            set { offerCost = value; }
        }

        [DataMember(IsRequired = true, Order = 9)]
        public bool IsRedeemed
        {
            get { return isRedeemed; }
            set { isRedeemed = value; }
        }

        [DataMember(IsRequired = true, Order = 10)]
        public bool IsExpired
        {
            get { return isExpired; }
            set { isExpired = value; }
        }

        [DataMember(IsRequired = true, Order = 11)]

        public string CasinoId
        {
            get { return casinoId; }
            set { casinoId = value; }
        }

        [DataMember(IsRequired = true, Order = 12)]
        public DateTime? OfferValidEffectiveDate
        {
            get { return offerValidEffectiveDate; }
            set { offerValidEffectiveDate = value; }
        }

        [DataMember(IsRequired = true, Order = 13)]
        public DateTime? OfferValidThruDate
        {
            get { return offerValidThruDate; }
            set { offerValidThruDate = value; }
        }
    }

    [CollectionDataContract(Namespace = "http://www.suninternational.com/schemas/SunIntSchemas/gaming/web/SlotsInfo/v1.01", ItemName = "item")]
    public class ErrorList : List<ProcessError>
    {
        public ErrorList() : base() { ;}
        public ErrorList(IEnumerable<ProcessError> collection) : base(collection) { ;}
        public ErrorList(int capacity) : base(capacity) { ;}
    }

    [DataContract(Namespace = "http://www.suninternational.com/schemas/SunIntSchemas/gaming/web/SlotsInfo/v1.01")]
    public class ProcessError
    {
        //private string errorCode;
        private string errorMessage;

        [DataMember(IsRequired = true, Order = 0)]
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }
    }
}
