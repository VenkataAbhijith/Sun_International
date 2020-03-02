using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace com.siml.gaming.slots.webinfo.contract
{
    [ServiceContract(Name = "SlotsInfoService",
        Namespace = "http://www.suninternational.com/schemas/SunIntSchemas/gaming/web/SlotsInfo/v1.01")]

    public interface ISlotsInfo
    {
        [OperationContract()]
        GetSlotMachineJackpotResponse GetMachineJackpots(GetSlotMachineJackpotRequest reqst);

        [OperationContract()]
        GetJackpotPayoutResponse GetJackpotPayouts(GetJackpotPayoutRequest reqst);

        [OperationContract()]
        GetPointsBalanceResponse GetPointsBalances(GetPointsBalancesRequest reqst);

        [OperationContract()]
        GetPointsTranansactionResponse GetPointsTrans(GetPointsTransactionRequest reqst);

        [OperationContract()]
        GetWapValueResponse GetWapVal(string WapId);

        [OperationContract()]
        GetWapHitResponse GetWapHits(GetWapHitsRequest reqst);

        [OperationContract()]
        GetGameCelebrationsResponse GetGameCelebrationsByValue(GetGameCelebrationsValueRequest reqst);

        [OperationContract()]
        GetGameCelebrationsResponse GetGameCelebrationsByMachine(GetGameCelebrationsMachineRequest reqst);

        [OperationContract()]
        GetMVGOffersResponse GetOffers(GetMVGOffersRequest reqst);
    }
}
