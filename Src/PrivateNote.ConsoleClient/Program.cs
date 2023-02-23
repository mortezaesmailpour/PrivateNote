
List<int> list = new List<int> {422, 5,43,3,354,2,24,2,54545,4,2454,5452,35,5,35};

List<int> Sortedlist = new List<int> {1, 5,15,30,120,202,245,54545};


Console.WriteLine(list[MaxIndex(list)]);


int MaxIndex(List<int> list)
{
    var result = 0;
    for (var i = 1; i < list.Count; i++)
    {
        if (list[i] > list[result])
        {
            result = i;
        }
    }
    return result;
}
int MinIndex(List<int> list)
{
    var result = 0;
    for (var i = 1; i < list.Count; i++)
    {
        if (list[i] < list[result])
        {
            result = i;
        }
    }
    return result;
}




List<int> Sort(List<int> a)
{
    List<int> sorted = new();
    while (a.Count>0)
    {
        // var maxIndex = MinIndex(a);
        // sorted.Add(list[maxIndex]);
        // a.RemoveAt(maxIndex);
        var max = a.Min();
        sorted.Add(max);
        a.Remove(max);
    }
    return sorted;
}
print(Sort(list));

void print(List<int> list)
{
    foreach (var item in list)
    {
        Console.Write(item+", ");
    }
    Console.WriteLine();
}






























//
// using PrivateNote.Api.Services;
// using PrivateNote.Tests.E2E.ApiLayer;
//
// Console.WriteLine("Hello, World!");
//
//
// var client = new PrivateNoteClient();
//
//
//
// //var response = await client.SignInAsync("Alice", "Aa@12345");
// //var response2 = await client.SignInAsync("Bob", "Aa@12345");
//
//
// //var response = await client.RsaSignUpAsync("TestUser",
// //    "<RSAKeyValue><Modulus>2mOZ2nHEgjy/U23xXM0Cvt8XsrOfmvxWtWzfn6L70YFXGcGk0Po1lQAhZxtnoz0+I9gyyGwaGR2Map+Mi3AUmZA1nhxncSVYr2k+0TETaq/KFUrms8psIb6tSdYnhz8yV2lwY8J1JEvopHE5j7b3HZmePdXWIjzPEbJKNFCeq4wZLZjBXc+xK3WpV9N+rBnGutsub4gKw5prVf6KlchxQH82VSP5Qo7vkowmtN+5d0xOxdrgqmT8m3bltYeRtexcBLQhIrn2fz9QKUKzqDj1Bca0Ef59/gw8WoWUOyKSKU1oa2+eIc3PimokDjgIvBaOHc9AS+5cETS+T91EWKroSQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>",
// //    "MF0F/R4NiHCjPyGc+82VeHs8lvGcberWH+vCwJCRDGE3+vhCCDhZrm0ctu1zi/8ffToCUSFM54P7PLFxJqdsXW95Uy4/hYNq8gKPHxa1f6Hz9X1fJwlWdta4f9Y0JNXAfyZe7P664rMqYfMymBYX9e5omvkZPWLkUti0EzNef5VTnj2T9zc1Ig8SRaNm7skOeUFHJtjg0zeTHRF7WJnx/BpSePYRWrY3DckHHFnTE5Kx2sjTPF85cDVo5GfSgXqnTi7G1SyCcxuGw4doEtJysTQRY948zh/CaKHYi1LZX+n8ABGbk3ETt0stZAzrCwWR80CxnhaZQY7nFsuq9lT55g=="
// //    );
//
//
//
// //var response = await client.SignInAsync("TestUser", "MF0F/R4NiHCjPyGc+82VeHs8lvGcberWH+vCwJCRDGE3+vhCCDhZrm0ctu1zi/8ffToCUSFM54P7PLFxJqdsXW95Uy4/hYNq8gKPHxa1f6Hz9X1fJwlWdta4f9Y0JNXAfyZe7P664rMqYfMymBYX9e5omvkZPWLkUti0EzNef5VTnj2T9zc1Ig8SRaNm7skOeUFHJtjg0zeTHRF7WJnx/BpSePYRWrY3DckHHFnTE5Kx2sjTPF85cDVo5GfSgXqnTi7G1SyCcxuGw4doEtJysTQRY948zh/CaKHYi1LZX+n8ABGbk3ETt0stZAzrCwWR80CxnhaZQY7nFsuq9lT55g==");
//
// var testUserPublicKey = "<RSAKeyValue><Modulus>2mOZ2nHEgjy/U23xXM0Cvt8XsrOfmvxWtWzfn6L70YFXGcGk0Po1lQAhZxtnoz0+I9gyyGwaGR2Map+Mi3AUmZA1nhxncSVYr2k+0TETaq/KFUrms8psIb6tSdYnhz8yV2lwY8J1JEvopHE5j7b3HZmePdXWIjzPEbJKNFCeq4wZLZjBXc+xK3WpV9N+rBnGutsub4gKw5prVf6KlchxQH82VSP5Qo7vkowmtN+5d0xOxdrgqmT8m3bltYeRtexcBLQhIrn2fz9QKUKzqDj1Bca0Ef59/gw8WoWUOyKSKU1oa2+eIc3PimokDjgIvBaOHc9AS+5cETS+T91EWKroSQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
// var testUserPrivateKey = "<RSAKeyValue><Modulus>2mOZ2nHEgjy/U23xXM0Cvt8XsrOfmvxWtWzfn6L70YFXGcGk0Po1lQAhZxtnoz0+I9gyyGwaGR2Map+Mi3AUmZA1nhxncSVYr2k+0TETaq/KFUrms8psIb6tSdYnhz8yV2lwY8J1JEvopHE5j7b3HZmePdXWIjzPEbJKNFCeq4wZLZjBXc+xK3WpV9N+rBnGutsub4gKw5prVf6KlchxQH82VSP5Qo7vkowmtN+5d0xOxdrgqmT8m3bltYeRtexcBLQhIrn2fz9QKUKzqDj1Bca0Ef59/gw8WoWUOyKSKU1oa2+eIc3PimokDjgIvBaOHc9AS+5cETS+T91EWKroSQ==</Modulus><Exponent>AQAB</Exponent><P>5bluBciuKiMNJANtNNcGqScwjSdjV9OzesW+IqmCEAUNHBDJYHfTkxOtxQ4VyYhNji757WI8FXALFzwqea48hTdTx5nsI4V/ZdChT8kO7joYrYWetkX32rZO8+ERFCBWpTn4oLMWkoFn6kHMjllsObBvw7a1x5qYs5L4YpdLTac=</P><Q>815DOxb7gpKxfdSQTQqLk11MonpWl/ayBApR2gmTDMZi6cekC6whmYm/GDuwVTrMYO6MyK5xD+BF3gtD9Dhsl4ZudRyhWrvnpjK2T+bmvkxRL8gQWYRq81TyiC/6QCdKyO1KOclIW7dn4WYg87wfvtIS/mHm/KcleaVL9H4yOI8=</Q><DP>sd4UEQhjyZ5gE01P5gTDVH9SeUhRA/SXV+z5na7vVGYE04Ev2rCMsakf955DQkp1+ivnOWTBLrwU6kWcgaBlnaPMC5TjIFHB5VxXOgZruAplhx0ZWPntwXs8wFm4NoQfckjjd9GSHZdylQ+jSWh1gmlY8Als0AKkTw3xoayREqs=</DP><DQ>D9DJZ7ano6Wq2TWOJyOPTIVcrJZsuDV0/iQ5i4ThxIvD89Ngis2l5Vd5TrbaT7+hdo8qIQSNHm4BvFtZEC72h6ZZ2UyCA+aAOGHFTU4BIs7M2+ERhu+/D48ur8EGb1LeXk5la3n0NNmna5N6grxwB7OZPmTYYQTvv4cJd5sX4ms=</DQ><InverseQ>DWAPW7bOWbypyYIG5EF7ldmnrEQapIgeePee30vYfQ/VqlBrUi9zW2A+JvNwCnsyA/QFrL0ERFPRht7Dv2Iub2suy3NcYZkxJ9P/gaiplRvG+UzIeXeKLsRmnw9lh1gIJa+AwrXVCZb/7GIkaeD/L+aiU8iDFLaYLRIqywEF0fs=</InverseQ><D>WJpCSHMR0/kLcrDPVOREMeZXOgRQQ9QO5cqHK8A3RHeiO+vpj3OEG7CS3L8sm/jz5h0H9XKOYBQx7OxQXB4b7XWpmz1Q5oHNalVbzAmKJCWv6lWaJeWBW2t96M/+94s3RGmd1IuHdTNq7pSVUu5bceHqZpIEL53WMQrRuknfJh+QApMK+aM4+f1JCJ9r0UuOUqJuB1eQb/z93RS88fXp1RhVhrOVq42coFCSO+aGcodhcey73+PLcEyzMCf49FUHLiRjlVOxcP32ICAw2O+LUUj8RWcogcNC4tvwpdsKGy3n+pLiyZKjzbS+GrovyQ++xOQS64U1/1DOavF7iV63RQ==</D></RSAKeyValue>";
// var testUserSign = "MF0F/R4NiHCjPyGc+82VeHs8lvGcberWH+vCwJCRDGE3+vhCCDhZrm0ctu1zi/8ffToCUSFM54P7PLFxJqdsXW95Uy4/hYNq8gKPHxa1f6Hz9X1fJwlWdta4f9Y0JNXAfyZe7P664rMqYfMymBYX9e5omvkZPWLkUti0EzNef5VTnj2T9zc1Ig8SRaNm7skOeUFHJtjg0zeTHRF7WJnx/BpSePYRWrY3DckHHFnTE5Kx2sjTPF85cDVo5GfSgXqnTi7G1SyCcxuGw4doEtJysTQRY948zh/CaKHYi1LZX+n8ABGbk3ETt0stZAzrCwWR80CxnhaZQY7nFsuq9lT55g==";
//
//
// var encryptedToken = await client.RsaSignInAsync("TestUser", testUserPublicKey, testUserSign);
//
// var token = RsaService.Decrypt(encryptedToken??"", testUserPrivateKey);
//
// var me = await client.WhoAmI(token!);
//
// Console.WriteLine(me?.UserName);










