using PrivateNote.Service;

namespace PrivateNote.Tests.E2E.ApiLayer;
public class AuthTests
{
    private readonly PrivateNoteClient _sut;
    private readonly RsaService rsa = new RsaService();

    public AuthTests() => _sut = new PrivateNoteClient();

    [Theory]
    [InlineData("Alice", "123")]
    [InlineData("Bob", "Aa@12345")]
    //[InlineData("User@example.com", "Aa@12345")]
    public async Task SignIn_ReturnValidToken_WhenUsernameAndPasswordAreCorrect(string username, string password)
    {
        // Arrange

        // Act
        var token = await _sut.SignInAsync(username, password);

        // Assert
        token.Should().NotBeNull();

        // Act
        var userInfo = await _sut.WhoAmI(token!);

        // Assert
        userInfo.Should().NotBeNull();
        userInfo?.UserName.Should().Be(username);
    }

    [Theory]
    [InlineData("alice", "123")]
    [InlineData("bob", "Aa@12345")]
    //[InlineData("User@example.com", "Aa@12345")]
    public async Task SignIn_ReturnNull_WhenUsernameAndPasswordAreInCorrect(string username, string password)
    {
        // Arrange

        // Act
        var token = await _sut.SignInAsync(username, password);

        // Assert
        token.Should().BeNull();
    }

    // RSA tests

    [Theory]
    [InlineData("Alice"
                        , "<RSAKeyValue><Modulus>ug9dfJwWG3zNs9WoEI2kJdrZrAhm5OonW566rCLsyKCaXOkuFvfA3OfE6VwL4Vxj/WfA6Ng3mvDZOCHd8OUKbTz8vAAzfCZebFN2ivjCllAYH6jdefeohUpG0h1KWJzxZ7zZoljS2T0l5MInjXzsPDREwPju2nlmUrPKWFs92ZvGFQhTftu5x/oMLH9Tz1hYlfC4uVleAJY1MZaCJir3GrvjEtkl7FRRKljUmxieDp4klOh48BdKLutJyjk2K/CoGQf06azPz/RjJbGifM/xKh4KLqAAJMpSwpy0+CTKn9A1hdNtpGtnm4U1+5TlhAiJEJeoGFoGDGZqJyCpYe4vZQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>"
                        , "N2rYbBSUOgx1LHDU9N9KHEuzsrdHhTYcbRpfI8/h80utds1OP+2p/yzN4rgpC3KZOxopjklrQrbOYAhrWRQ+Gp3hCTpOt+esBC2C2nMeRwfU7s3R0VVVQDkttArxCO3DuOEDlk9diU/15ZKGfG3ziyy8UypjiXWTL9O7jRIDFInB79sEDwl0S+x8c8prsmdy2kVKRBAwVheU4HQSDEw1shjyj+UY1C52aux4hZU46Rtj4iFh83rx02tgtDy9MWRlN7oZuBwCU75VKAnmcwoBXKAeyv/rof2/LKE9SeUWkNXcNMGuGudPepo/A58fS/negixDFlsmLSUIU2v3xTFH/g=="
                        , "<RSAKeyValue><Modulus>ug9dfJwWG3zNs9WoEI2kJdrZrAhm5OonW566rCLsyKCaXOkuFvfA3OfE6VwL4Vxj/WfA6Ng3mvDZOCHd8OUKbTz8vAAzfCZebFN2ivjCllAYH6jdefeohUpG0h1KWJzxZ7zZoljS2T0l5MInjXzsPDREwPju2nlmUrPKWFs92ZvGFQhTftu5x/oMLH9Tz1hYlfC4uVleAJY1MZaCJir3GrvjEtkl7FRRKljUmxieDp4klOh48BdKLutJyjk2K/CoGQf06azPz/RjJbGifM/xKh4KLqAAJMpSwpy0+CTKn9A1hdNtpGtnm4U1+5TlhAiJEJeoGFoGDGZqJyCpYe4vZQ==</Modulus><Exponent>AQAB</Exponent><P>4UvK9/wRToTMSoP00rUlUJrIHGcqJ/ZcpydkZHzlQO8CvTmSlET7iqGJ1FIwB4Vwq1xu2IqnDNvAT2O1RJcwZcOoTS8XWL8gf/xQ5UaqFrEnwTvTatvxtRGTRk+PvzQQO/Ygf5buHfc8jIQrKRHUYt2zcswbjFiVCBgATFM5rbs=</P><Q>02qxDD0WFoYXksmgauYoKitXTLi3f0HBSFUs86XHXdRBYZhzpbDyc5MjJooDQE3OdjRvsMnxmdzFehgC1esa6N0hGbSlot2I+1Sul8Yfl/dqcL4qkOqOX5VBjS7r4cilotwVIyrVA+jjNmm2p9ckBvd/lIHmUqPXcMcIQGs9NV8=</Q><DP>RHAFo/cKOtPUSv5yrF8iiq26BAbCo4kO9CffMzdNXMA+EMUgZbHex2nyuHCO7nn2k9dsRJM504odjQA43DEhHoik421epjLI6Jf46uzgVixSazyFKHzPm7VPC5i3jdl+5PgLPzbV9nxYBEdR5RPpdG7sR/8Aj02JWAASdx0A9Gk=</DP><DQ>cequRYrwyOgt8ZElGBS2FarO2m85qJ9Ut1X1578vArbpF95eiiwjJ/VjXlPFVrPWCMlOWXMm9KIJ4jTn6j5JfKoSONim23AFbG9/uBYwVeEiAGfnZbiNSKGS9bEPttQ3s95koVNl/jpRm7MwkZWSOxLHMs0fvu27m3ATWSqvi38=</DQ><InverseQ>bXGJbpAe/zaP/lw+v/+kfJHLZlscLFgJviDuNOeMnZpgxYSjFTHkRpSj5T92jdH7Wv/IUfjlrpZSmRvZDLVsIR8Hw5KJM/8CtPfK6inHFJi7iGbT+Tfz/SRqrgECIdCFIPgIPCv3cr+GxtyVJQxDa2ZZLqraNpxgwJdINT9g84g=</InverseQ><D>C+jd4BMRqzLC7Ab6Gg0m+7FVi5iCFZcTa97uLgSSSYlWOCpI4/l54+oGBbX5oLRMRjU0+PKWJJnUHa61SS0wDKwBtXsB0KmYE7EH/n9L3VlFIgmqG5uJsvZ9MAfl8WQiY7SH7cdnv7YVTd5Lr9fWO0hROlT03RE2RobxpdQ5Ukt1Xyr2DA0t+2iHGYe8Yhpr9lh8kuNB1PU16Q9qkAyvVPH6ovedWQlVuBoG9xww993uEH0eVEK37eUzYFMNVT+bSYIfjyu51oqt6kR49wG/khFEhqH0S6DO9vJFcYctfh46fIbWLMn+pK8qvS7AW+idRyz6ydj4ryIdmrLIaoJBZQ==</D></RSAKeyValue>"
                        )]
    //[InlineData("User@example.com", "Aa@12345")]
    public async Task RsaSignIn_ReturnValidToken_WhenPublicKeyAndSignatureAreCorrect(string username, string publicKey, string signature, string privateKey)
    {
        // Arrange

        // Act
        var encryptedToken = await _sut.RsaSignInAsync(username, publicKey, signature);

        // Assert
        encryptedToken.Should().NotBeNull();

        // Act
        var token = rsa.Decrypte(encryptedToken, privateKey);

        // Assert
        token.Should().NotBeNull();

        // Act
        var userInfo = await _sut.WhoAmI(token!);

        // Assert
        userInfo.Should().NotBeNull();
        userInfo?.UserName.Should().Be(username);
    }

    [Theory]
    [InlineData("alice", "123", "abc")]
    [InlineData("bob", "Aa@12345", "abc")]
    //[InlineData("User@example.com", "Aa@12345")]
    public async Task RsaSignIn_ReturnNull_WhenPublicKeyAndSignatureAreInCorrect(string username, string publicKey, string signature)
    {
        // Arrange

        // Act
        var token = await _sut.RsaSignInAsync(username, publicKey, signature);

        // Assert
        token.Should().BeNull();
    }
}