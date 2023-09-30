using System.Collections.Generic;

namespace TellDontAskKata.Main.UseCase.Requests
{
    public class SellItemsRequest
    {
        public IList<SellItemRequest> Requests { get; set; }
    }
}
