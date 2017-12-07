﻿using Autofac;
using Common.Log;
using Lykke.Service.LiteCoin.API.Core;
using Lykke.Service.LiteCoin.API.Core.Address;
using Lykke.Service.LiteCoin.API.Core.BlockChainReaders;
using Lykke.Service.LiteCoin.API.Core.Fee;
using Lykke.Service.LiteCoin.API.Core.Settings.ServiceSettings;
using Lykke.Service.LiteCoin.API.Core.WebHook;
using Lykke.Service.LiteCoin.API.Services.Address;
using Lykke.Service.LiteCoin.API.Services.BlockChainReaders;
using Lykke.Service.LiteCoin.API.Services.BlockChainReaders.InsightApi;
using Lykke.Service.LiteCoin.API.Services.Fee;
using Lykke.Service.LiteCoin.API.Services.WebHook;
using Lykke.SettingsReader;
using NBitcoin;

namespace Lykke.Service.LiteCoin.API.Services.Binder
{
    public  class ServiceModule:Module
    {
        private readonly ILog _log;
        private readonly IReloadingManager<LiteCoinAPISettings> _settings;
        public ServiceModule(IReloadingManager<LiteCoinAPISettings> settings, ILog log)
        {
            _log = log;
            _settings = settings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            RegisterNetwork(builder);
            RegisterFeeServices(builder);
            RegisterAddressValidatorServices(builder);
            RegisterInsightApiBlockChainReaders(builder);
            RegisterWebHookServices(builder);
        }

        private void RegisterNetwork(ContainerBuilder builder)
        {
            NBitcoin.Litecoin.Networks.Register();
            builder.RegisterInstance(Network.GetNetwork(_settings.CurrentValue.Network)).As<Network>();
        }

        private void RegisterFeeServices(ContainerBuilder builder)
        {
            builder.RegisterInstance(new FeeRateFacade(_settings.CurrentValue.FeePerByte)).As<IFeeRateFacade>();
            builder.RegisterType<FeeFacade>().As<IFeeFacade>();
        }

        private void RegisterAddressValidatorServices(ContainerBuilder builder)
        {
            builder.RegisterType<AddressValidator>().As<IAddressValidator>();
        }

        private void RegisterInsightApiBlockChainReaders(ContainerBuilder builder)
        {
            builder.RegisterInstance(new InsightApiSettings
            {
                Url = _settings.CurrentValue.InsightAPIUrl
            }).SingleInstance();

            builder.RegisterType<InsightApiBlockChainReader>().As<IBlockChainReader>();
        }

        private void RegisterWebHookServices(ContainerBuilder builder)
        {
            builder.RegisterInstance(new WebHookSettings
            {
                Url = _settings.CurrentValue.EventsWebHook
            }).SingleInstance();

            builder.RegisterType<WebHookSender>().As<IWebHookSender>();
        }
    }
}
