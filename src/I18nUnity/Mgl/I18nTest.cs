namespace Mgl
{
    using NUnit.Framework;

    public class I18nConsoleAppStub
    {
        public static void Main()
        {
        }
    }

    [TestFixture]
    public class I18nTest
    {
        [SetUp]
        public void Init()
        {
            I18n i18n = I18n.Instance;
            I18n.Configure("Locales/", "en-US");
        }


        [Test]
        public void TestLocale()
        {
            I18n i18n = I18n.Instance;
            Assert.AreEqual("en-US", I18n.GetLocale());
        }

        [Test]
        public void TestConfigureLocale()
        {
            I18n i18n = I18n.Instance;
            I18n.Configure("Locales/", "fr-FR");
            Assert.AreEqual("fr-FR", I18n.GetLocale());
        }

        [Test]
        public void TestSetLocale()
        {
            I18n.SetLocale("es-ES");
            Assert.AreEqual("es-ES", I18n.GetLocale());

            I18n.SetLocale("en-US");
            Assert.AreEqual("en-US", I18n.GetLocale());
        }

        [Test]
        public void TestHello()
        {
            I18n i18n = I18n.Instance;
            I18n.SetLocale("en-US");
            Assert.AreEqual("Hello", i18n.__("Hello"));

            I18n.SetLocale("fr-FR");
            Assert.AreEqual("Bonjour", i18n.__("Hello"));

            I18n.SetLocale("es-ES");
            Assert.AreEqual("Hola", i18n.__("Hello"));
        }

        [Test]
        public void TestZero()
        {
            I18n i18n = I18n.Instance;
            I18n.SetLocale("en-US");
            Assert.AreEqual("No credits", i18n.__("{0} credits", 0));
        }

        [Test]
        public void TestOne()
        {
            I18n i18n = I18n.Instance;
            I18n.SetLocale("en-US");
            Assert.AreEqual("1 credit", i18n.__("{0} credits", 1));
        }

        [Test]
        public void TestNegativeOne()
        {
            I18n i18n = I18n.Instance;
            I18n.SetLocale("en-US");
            Assert.AreEqual("1 credit", i18n.__("{0} credits", -1));
        }

        [Test]
        public void TestOther()
        {
            I18n i18n = I18n.Instance;
            I18n.SetLocale("en-US");
            Assert.AreEqual("15 credits", i18n.__("{0} credits", 15));
        }

        [Test]
        public void TestNegativeOther()
        {
            I18n i18n = I18n.Instance;
            I18n.SetLocale("en-US");
            Assert.AreEqual("15 credits", i18n.__("{0} credits", -15));
        }

        [Test]
        public void TestBasicReplacement()
        {
            I18n i18n = I18n.Instance;
            I18n.SetLocale("en-US");
            Assert.AreEqual("Hello Tester, how are you today?", i18n.__("Hello {0}, how are you today?", "Tester"));
        }

        [Test]
        public void TestMultipleReplacement()
        {
            I18n i18n = I18n.Instance;
            I18n.SetLocale("en-US");
            Assert.AreEqual
                (
                "Level The Lion's Den time: 00:44:23 score:10,0000 ammo:12 player:One",
                i18n.__(
                    "Level {1} time: {0} score:{3} ammo:{4} player:{2}",
                    "00:44:23",
                    "The Lion's Den",
                    "One",
                    "10,0000",
                    "12"
                )
            );
        }

        [Test]
        public void TestZeroMonkeys()
        {
            I18n i18n = I18n.Instance;
            I18n.SetLocale("en-US");
            Assert.AreEqual("There are no monkeys in the tree.", i18n.__("There is one monkey in the {1}", 0, "tree"));
        }

        [Test]
        public void TestOneMonkey()
        {
            I18n i18n = I18n.Instance;
            I18n.SetLocale("en-US");
            Assert.AreEqual("There is one monkey in the tree.", i18n.__("There is one monkey in the {1}", 1, "tree"));
        }

        [Test]
        public void TestMultipleMonkeys()
        {
            I18n i18n = I18n.Instance;
            I18n.SetLocale("en-US");
            Assert.AreEqual("There are 27 monkeys in the tree!", i18n.__("There is one monkey in the {1}", 27, "tree"));
        }
    }
}
