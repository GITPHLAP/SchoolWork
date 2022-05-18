using NUnit.Framework;

namespace MyFirstUnitTest.UnitTests
{
    [TestFixture]
    public class ReservationTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue()
        {
            var madebyUser = new User { IsAdmin = false };
            var reservation = new Reservation(madebyUser);

            var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });

            Assert.True(result);
        }

        [Test]
        public void CanBeCancelledBy_SameUserIsMadeBy_ReturnsTrue()
        {
            var madebyUser = new User { IsAdmin = false};
            var reservation = new Reservation(madebyUser);

            var result = reservation.CanBeCancelledBy(madebyUser);

            Assert.True(result);
        }

        [Test]
        public void CanBeCancelledBy_AnotherUser_ReturnsFalse()
        {
            var madebyUser = new User();
            var reservation = new Reservation (madebyUser);

            var result = reservation.CanBeCancelledBy(new User ());

            Assert.False(result);
        }
    }
}