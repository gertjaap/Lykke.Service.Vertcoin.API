﻿using System;
using System.Collections.Generic;
using System.Text;
using NBitcoin;

namespace Lykke.Service.LiteCoin.API.Core.TransactionOutputs
{
    public class CoinWithSettlementInfo : Coin
    {
        public bool IsSettled { get; set; }

        public CoinWithSettlementInfo(OutPoint fromOutpoint, TxOut fromTxOut, bool isSettled) : base(fromOutpoint, fromTxOut)
        {
            IsSettled = isSettled;
        }

    }
}
