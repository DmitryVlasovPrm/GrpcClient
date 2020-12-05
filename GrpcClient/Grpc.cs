using System;
using Grpc.Core;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace GrpcClient
{
    public static class Grpc
    {
        public async static void SendMessage(string serverUri)
        {
            try
            {
                //AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                //var httpHandler = new HttpClientHandler();
                //httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

                var cacert = File.ReadAllText("keys/server-cert.pem");
                var ssl = new SslCredentials(cacert);
                Channel channel = new Channel("127.0.0.1", 8888, ssl);
                var client = new DataSender.DataSenderClient(channel);

                // Отправка данных батчами
                using var call = client.SendRawData();
                while (Program.Countries.Count != 0)
                {
                    var curCountry = Program.Countries.Dequeue();
                    var data = new SendingData { Id = curCountry.Id, CountryName = curCountry.CountryName, CountryPopulation = curCountry.CountryPopulation,
                    CountrySquare = curCountry.CountrySquare, CapitalName = curCountry.CapitalName, CapitalFoundation = curCountry.CapitalFoundation,
                    TotalGdp = curCountry.TotalGDP, HumanGdp = curCountry.HumanGDP, GdpYear = curCountry.GDPYear, LanguageName = curCountry.LanguageName,
                    LanguagePrevalencePlace = curCountry.LanguagePrevalencePlace, RegionName = curCountry.RegionName, RegionCenter = curCountry.RegionCenter};
                    await call.RequestStream.WriteAsync(data);
                }
                await call.RequestStream.CompleteAsync();
                var reply = await call.ResponseAsync;
                Console.WriteLine(reply.ReplyStr);

                channel.ShutdownAsync().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка во время отправки сообщения. {ex.Message}");
            }
        }
    }
}
