﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lykke.Service.LiteCoin.API.Core.Fee;
using Lykke.Service.LiteCoin.API.Services.Fee;
using Moq;
using NBitcoin;
using Xunit;

namespace Lykke.Service.LiteCoin.API.Tests
{
    public class FeeFacadeTests
    {
        [Fact]
        public async Task CanCalculateFeeForTxBuilder()
        {

            PrepareNetworks.EnsureLiteCoinPrepared();

            var feePerByte = 100;
            var feeRateService = CreateFeeRateFacade(feePerByte);

            var feeFacade = new FeeService(feeRateService.Object);

            var txBuilder = GetTestTx(Network.RegTest);

            await feeFacade.CalcFeeForTransaction(txBuilder);
        }

        [Fact]
        public async Task CanCalculateFeeForTx()
        {

            PrepareNetworks.EnsureLiteCoinPrepared();

            var feePerByte = 100;
            var feeRateService = CreateFeeRateFacade(feePerByte);

            var feeFacade = new FeeService(feeRateService.Object);

            var txBuilder = GetTestTx(Network.RegTest);

            await feeFacade.CalcFeeForTransaction(txBuilder.BuildTransaction(true));
        }

        
        private TransactionBuilder GetTestTx(Network network)
        {
            var tx1 = Transaction.Parse(
     "0100000013b86d4167fea506b63f010fbdfa492990d50654994afbdea7915d55518d486191020000006b483045022100d86839efe810a0c5ac5a1f7769e8404c1c19167c6513337b8c3a3f2c90f37a34022068049ddcf5978bc00ac55fc2ed21fa5e33b159ba731f4d2660b262a831d04c01012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffff5879c08154d0a3559bc043437e16f3664f3d174d4690736e8e3029a13fbb0680020000006b483045022100b76c883697f9bcc84203a50e2ce03ea5eb6aad5510e64592076f611c8f37cf980220142487d68db675558ccfa1b603018e39534bd81e23a848ec91b5da6031c47706012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffff4307db63476394a7a329027d915b216d483d7531bac2498c53bf389706e49f34020000006a473044022030791cf875e3b6928d08beaf441b128a61b00bff6a8c7dad412fbea7b47fe27a022047166ba2fb654d00052b56e7f7ba56b3dba3663eacad4ee99ea1a01497a8e407012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffffdae24ed88eafe82d2a5e06401ac49ccbf2da623515188ef225c04a7fb9ce03b3020000006b4830450221008fe305e96cf77df356d5c27a343c74f6f8aad38c2f4d2ff3dcbb57c10d886320022029f0ef087619d5ea38ae87a5cc1fe4ed27848ab574c60645f89c7305f124f122012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffff8b5285cffa6882352197f94c34eb9f4402bfe70a1437590f830ca06c662304a2020000006a47304402204ff53947aa67a7d128e1a36e3654ebc5bae4b2147eb7d651254a135fe0cda49b022074664d8c3a3e18e62a50a191a91d36238b2c7cd166ae88054f14083501ac8621012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffff5f7b4f51d86a1fb77f002c9a5a15e077b0f8d54f7be75cdfc4b86e4c2d137b14020000006a4730440220294c996d0a3cb17d23f33d6ec3e8d94893ce3ead1fcaa5e7d3b8e3f8525753d202205cb91384445f65d334e700617299b1667466ff576aeced55d20c52c164eb9898012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffff60327c04d5ce8f196df339ae949278691c6d855669a3ea15c357fd530d19aca8020000006a473044022073cf6cdd5cf37af98fa6284f34311b44647ff533e023622a7962c3f9932bf57b0220213303c4d01422f2f1e97ba66e2a0369a144d294e4c1d574ced61900f7b1bcf4012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88fefffffff2a3512c8f0f12161c473009c98c971daed525226d035e2ef3eb33a99732c18a020000006b4830450221009ac65fad7b549ac6c02cd2ff16f585c831777d9af9774a0fb2576cb985bbddbe022018b05e5eed3e4be955751fe5fa84f0491b0ee58a12d7d66ccf87f6751ca1bdbb012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffffcaa534fe6f69349d13968a3ca7ac36f3428262f79345736b7e30eb3b80525a08020000006b4830450221008c36bff99929eab0f6f516a87b3120ab3a38381cb36f502eea2136007b3f0d6702205edf3bdf52a9a155caa2d5fb5bc4ef389a2fe826c98a2cef9fcdc9dbe920d081012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffff29fbd516d9f0688e10d96808f567768a327e688d2bfa0a9b4873a86dfa94914f020000006a47304402203e9ba6d1acc8fe0ec7b8464456002c38898295883a7689c392cdafc9417667400220459667511572eedeb100f58e4b894ca386197a9dbd30d1a672789e932a06851c012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffff88fa2f8ef3b12989fdd5f7b99aee1e913f7009ba03128906db0efc43ea543eb7020000006b483045022100d5934e6aa6ce55b4cdf3edb9c3bd064920f2bc68363c87a362004df09641babd022005e6ae20dd0789c1cdc65e108edf10452edbf50d662a8da60ca2413bdcf6b82f012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffff5a5cb8a64a0956c91f517031c851cfb450ee7bb13e87efcffdd1e8df2aa546a4020000006a47304402206690a37a6b2b6182b1728874953a440c68371d9e04678d72b3266ee9ecb9a50802203906a240d89879ddcf5395b424b2e96741413e881122dcf7b1d2c22ddec8be01012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffff32f4b8803e89d0b7848aac3f7353edc429a8888bd3065c2c74843f8ff37f4b5d020000006b483045022100928c05bc87646c1251999ae7c6d47f73f66e47c2828e62a151c6be4dd205960e02203fa6fe784c35e069156345a754793cb3c312b069b30f9942aafb6aab54bbeb1e012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffff7483042657fe8a5ca575daeb24e70041b214da3300103d85de6579271a62b24e020000006b48304502210093d9fb6bac8920c0e69e138cde3efff4a861417b21a8383d5a0c2e3e72a871e702207f7a1100ae8d0f167c589c8bd3d13ec74a5363ade35d23d7a8ecfd30b78605c0012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffffd281b2798b9af987b5a0c080905ff37bffa4855650110752e53efbbb8bb55926020000006a47304402204098bad4495e5e726d6e4efc40e43c436739f102b13e4bd75ce7379a97bce5ee02203f25d4dfd36d7c307c9a8e9916e7b653c9f5e4761a366317ea3dd93d1fb3b485012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffff66fa0b1a25a81e0ac1a54a28007df7a742e69801999c0d221e4e6673a970df62020000006a47304402206517c585f7ff9ba076865d49f294332877905894c8f4a89eceb0d96c0648a64c02207df12c3090e6b03b03e3e1d9406e617215677fbcbd1f6e1cbcb5ff394c4d34b6012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffff708d7c131f1668f62021fbedfcd8aca5c75fdfb4bd9b96eb06be46642937c521000000006b483045022100f64e075555674764002848f52e8ef4939f7c801a8dd055983fc3a94465dd7843022062b7afb225ac7d4b35a915c8c05b9a1e504ee3016de0cca2bd29d79f16328733012103bd9abe43789afd5de8defc86904e210fd4e20a7b8bfae58237e796e4ceadf415feffffffe551fc26491234361d4ddd9c3d0f1c1a4eda90511453e0a114c6a7a290115ac8020000006b483045022100d97780a8b1bcbe2926e739ac83427af561053638b4070fd39cfd1f9cb69df7ff02204db0addb3e13804912e963fe3f9c6ddb02ea913505cde75b89e23701d53e9f73012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffff843c3273fdf42cd991ac129f0994647fe0ef18d4162620f6fa6d64b3b59761a6020000006a47304402205a1e5a057c2f1efc20c8d0a8c9733f579f5badd47eea11d582a83e6c5dd1bd5c022021b4d68063277b594db7bc2b9e7d92b9459a323b9098587d8102a022979909cb012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffff029cc72c01000000001976a9149aa9e40d9a17c30b136af008999f34c09b6f78e088ac00e9a435000000001976a9140bac38e69ebc5c594538fe24010c8cc8c20c851788ac4e380400");
            var tx2 = Transaction.Parse(
                "01000000134c719e1c9c0e51977a17a005939cbf95463f4f69be745613435d1442f9965db2000000006a4730440220763cade9bb6284991569f201bcb18494f13b25b3d1b426f32d08c8032231db76022076c13d33ea5863a4cf4fe7fc392cfd716afd8f6c6b70ed881f66538cb8ae71220121024bee29945a5d3bed699e22ea97a41ca9b2ec73429a6de5c1d9f4a0faab54cf21feffffffb7461ae1b31bf4d9a6165a90d9b5f93267b4c131ed181ef271bdc42fb2c59435020000006a473044022065dd44e8bad31386846f55623307defdb01fa2e7e4103ee079c3fc723e85ed110220753112fa72b5ebe495c2b88623a2c97ba9277d82bbdcaae3bf58f31773765a7b012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffff505f8a44874754f838e9c5a7303f923bfd091cc4882b5d64ae69287dc9fda24f020000006a47304402201a13c1f010833c934f3f811276850f3183fa3d1de98c5ac15dc46d9dc6bedf3c02204987a13ba7c5ba10d8387d6159b9b4abccf60dfbfa595411fc41552a33920c74012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffff24bfd4ef4a588721f7bb7ba42fc3f1c8d2a8d0ea3692df934b2d684c29509027020000006b483045022100b7ddb1f01ee88cfeb095581409ec993b749ec3d57a619e2716fe419c53b46ec8022013deb237ae6c76aa2371e49c4a8152cf1186f2f1c6824fa694940a1e5c26e161012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffffdfe763644e59d39f3152ce74ddca72035b74cf64b425606c16c2957a27b94ecb020000006a4730440220223977e9692703b267c5719b5beead737bbb60ba05bd6cbb5dcf3bd69dc6163e022006a5f5e5f40b6c6b3aa35f557b7953b6b618a1c4ac6e4ebbc1b91d3b5bd6c9ef012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffffe653ab83fed0a45d72c8f61f9b17d8857aa9614b428338714af095f81f5d66e1020000006a4730440220565d0d8bb88fac36f0b603882a75f369c4931bbe7c00ecd509015c9c8ddb67b502201de805f879d88026c8589e34dfa35b1573b441836a474448b4e5b5c366f30f80012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffff124426ccde27b21a93d225d0220a75cc669031ea2fee570cf9a4ece5da44ca0a020000006a4730440220543bc3abc785ab65fcb5779f02bbbd36e9c7a60466e0801c372ed11483e28b23022005587f45d03b426f83b9e12026d33d11f7338fa74a376edba35b6a2f3860b20c012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffff170f686bada507adbdac6c00c32481d29c9bea2d60cf59763a68a7a1998de0d1020000006a473044022054344f18b8736229d793f504ab21ff08ecb856df3611ad97de596ab1a1976fdf022065378c24b0507058e6594e3458f3ad932fb643ee58b5d7211f781cd3d33c8e6a012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffff1129351ac67d4784f7817f64b1c1c7d7dcf9536e0185c584fccc575a7a638ef0020000006b4830450221008915f7ae6681c391533de93fe21a275cb14e3399cfbc60b20df8d44b7b4d738a022053adebbee170ca54d83760fef2405c9007d520c6dafeaa672eec8ac1af6b326b012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffff25511ab5687edccf4434ea01fe7833031981d302d003d85f7a2b1c4eeb32e0ea020000006a47304402204cf0c5561d5585ce41ad0db66cb2312c22b703ea41c86e078f769b75ebcb6d6002202560482d0966062ba0e06a05221caf841f51ba99d2c8d54082e7c243505dcd76012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffffdf6d04ded11ee9e64b0dbe56bdb65cef752f5bdff48d4a6c0d729737f03ffb69020000006b483045022100b1098476ab66f451fdc93cff5bf93e67e8bc3a6cffd89d5d1292bfd21ae1c1cf022054980749a745c82bf9d64d0582d147b83f7afbc204a9b26f3ac65df052845654012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffff06db0a031227370c34e68ab778a71de8e2a3c2d1a767e182621369aac8c01861020000006b483045022100ed2a4645dbfdc61fba2e7296cc87abda054db1893443953e04e6d1b903e8175c0220536ce07b9bc75bebf665ba96aa901fad268d9633e577930410b5bc00674aef1a012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffff9b31ccce4ed9a1b44b306bb34a8b806df24189efdc90fe715fc3bbf04a254612020000006b483045022100bd4e627a193798c3273212fb189180f4f5c285d058d89c83bede6cf5dc17f14b022035e2661984be53b5dc4b384cc858c3bbe731938293978106d4a8d053b623ffc1012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88fefffffffe88c55f8f46eab273a8b8fe67bc1e50e70b54b6efaf66bd389de0201804b9a9020000006b483045022100915c8dd6b3d2a9c0e4db87e8ca634864aebcefc8b9100b816beba82b4cf44ab6022000f9cd6940e4e356d96ee1779da1fc270d8cd8f03384c287410c992cef59864a012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88fefffffff6175b8e065876da4d465687ee9958dc45eb1f3022e18160846e57fced2ce5fc020000006a47304402204b7fd1a81fad76c7117405001bbdf98231ec20bdbc4c6337dccd4850c313695c0220600f6438f27c5a870323b26a2aff8ee36dfba8dc4f6fc8c130b16c6711950b10012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffff490a75cdd8fdb4d5ec5132e0f5c15cd5e015c634877270fa9f0ca3c2c7f7ee95020000006b4830450221009795d4e0c8fadcdf3f8bff6a8876acefe517f50f9d4b6a2436abebd5665e290702206f1d7f23b65d17eb5ede6e9960a9891e316e5ba44d8f80a9f2bf0c9871f160fc012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffff0ee84bf08155d19262ff8855eef03d3cd25cfdf27eeef27e0a26fdd27a59c10f020000006a473044022009b0f7faa46cdbad7a8f43647878e6ec38dc169807c8b21b1def55f4a9ceaaa302204bf28cd2b3b2a1cbfbe982abd1a7d6bbe7b49462e13cda7e81ad83fc3bda722d012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffffedbda4b6b339029f1e36a7b4cdce7fc9d2c60ac176f8ee38d4bf9f0230a6c805020000006a47304402202b8b0f9f83dc3453ff32aed14773e125ef785af2e3e32d52c75f826b4a9ae0e102201f4485612512c4f1398bf047f0f318e7f56b0e12ccab9e7f54d62e5b0ca59c8c012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffff1ecc5a76344db3ac2e7299bd7bdf8f326656bb168306e25ec0dbdcea1ad87f29020000006a473044022045e1dffffa7298a3ae2c6e8f656ddf5d4ef2a73bc07308613c96133b62ba36d302202d660c6cd562393f06da67432a8aed3ac3daf62eed6545d9da30325c1957bd01012103f135ffd134a723e681df952eca135ded6ea3bc007907344c8f951fcd7f9f5f88feffffff0200e9a435000000001976a9140bac38e69ebc5c594538fe24010c8cc8c20c851788acec905700000000001976a914108d575ee09167da482e49a29861ba59712acb9788ac4e380400");
            var coins1 = tx1.Outputs.Select((o, i) => (ICoin)new Coin(new OutPoint(tx1.GetHash(), i), o))
                .ToArray();

            var coins2 = tx2.Outputs.Select((o, i) => (ICoin)new Coin(new OutPoint(tx2.GetHash(), i), o))
                .ToArray();


            var sender = Key.Parse("cRPs64pU9MQefr4npaKLuPNB4N89xGmgHvW2CFWhFPeekXLDyoKs");
            var receiver = new Key();
            var builder = new TransactionBuilder()
                .AddCoins(coins1)
                .AddCoins(coins2)
                .Send(receiver.PubKey.GetAddress(network), "5")
                .SetChange(sender.PubKey.GetAddress(network));

            return builder;



        }

        private static Mock<IFeeRateFacade> CreateFeeRateFacade(int feePerByte)
        {

            var result = new Mock<IFeeRateFacade>();

            result.Setup(p => p.GetFeePerKiloByte())
                .ReturnsAsync(feePerByte);

            return result;
        }
    }
}
