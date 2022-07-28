namespace PrivateNote.Tests.E2E.ApiLayer;
public class AuthTests
{
    private readonly PrivateNoteClient _sut;

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
}