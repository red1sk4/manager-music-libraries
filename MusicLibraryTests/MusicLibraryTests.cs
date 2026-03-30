using MusicLibrary;
using Xunit;

namespace MusicLibraryTests
{
    public class MusicServiceTests
    {
        [Fact]
        public void Add_ValidTrack_ShouldAddItem()
        {
        
        var service = new MusicService(); 

        
        var result = service.AddTrack("Imagine Dragons - Believer");

        
        Assert.NotNull(result);
        Assert.Equal("Imagine Dragons - Believer", result.Title);
        }

        [Fact]
        public void Add_EmptyTitle_ShouldThrowException()
        {
            var service = new MusicService();

            
            Assert.Throws<ArgumentException>(() => service.AddTrack(""));
        }
    }
}
