﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lykke.Service.LiteCoin.API.Core.Wallet
{
    public interface IObservableWallet
    {
        string Address { get; }
    }

    public class ObservableWallet:IObservableWallet
    {
        public string Address { get; set; }

        public static ObservableWallet Create(string address)
        {
            return new ObservableWallet
            {
                Address = address
            };
        }
    }
    public interface IObservableWalletRepository
    {
        Task Insert(IObservableWallet wallet);
        Task<IEnumerable<IObservableWallet>> GetAll();
        Task Delete(string address);
    }
}