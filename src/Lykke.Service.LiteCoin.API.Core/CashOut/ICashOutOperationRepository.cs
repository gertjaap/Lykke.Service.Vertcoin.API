﻿using System;
using System.Threading.Tasks;

namespace Lykke.Service.LiteCoin.API.Core.CashOut
{
    public interface ICashOutOperation
    {
        string OperationId { get; }
        DateTime StartedAt { get; }
        DateTime? CompletedAt { get; }
        string WalletId { get;  }

        string AssetId { get; }

        decimal Amount { get; }

        string Address { get; }

        bool Completed { get; }
        string TxHash { get; }
    }

    public class CashOutOperation: ICashOutOperation
    {
        public string OperationId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string WalletId { get; set; }
        public string AssetId { get; set; }
        public decimal Amount { get; set; }
        public string Address { get; set; }
        public bool Completed { get; set; }
        public string TxHash { get; set; }
        
        public static CashOutOperation Create(string operationId, 
            string walletId, 
            string address, 
            decimal amount,
            string assetId,
            DateTime startedAt,
            string txHash,
            bool completed = false,
            DateTime? completedAt = null )
        {
            return new CashOutOperation
            {
                Address = address,
                Amount = amount,
                AssetId = assetId,
                CompletedAt = completedAt,
                Completed = completed,
                OperationId = operationId,
                StartedAt = startedAt,
                WalletId = walletId,
                TxHash = txHash
               
            };
        }
    }


    public interface ICashOutOperationRepository
    {

        Task Insert(ICashOutOperation operation);
        Task SetCompleted(string operationId, DateTime completedAt);


        Task<ICashOutOperation> Get(string operationId);
    }
}
