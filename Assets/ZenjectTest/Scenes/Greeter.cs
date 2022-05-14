public class Greeter : Zenject.IInitializable
{
    private readonly string testJson = "[\n" +
                                             "{ " +
                                               "  \"ID\" : 1,\n" +
                                               "  \"TestStringField\" : \"Test String\",\n" +
                                               "  \"TestFloatField\" : 3.14\n" +
                                            "},\n" +
                                            "{ " +
                                             "  \"ID\" : 2,\n" +
                                               "  \"TestStringField\" : \"Test String 2\",\n" +
                                            "  \"TestFloatField\" : 6.28\n" +
                                            "}\n" +
                                       "]";

    private class TestData : KahaGameCore.Common.IGameData
    {
        public int ID { get; private set; }
        public string TestStringField { get; private set; }
        public float TestFloatField { get; private set; }

        public TestData() { }
        public TestData(int id, string testString, float testFloat)
        {
            ID = id;
            TestStringField = testString;
            TestFloatField = testFloat;
        }
    }

    private readonly KahaGameCore.Main.Core m_core;

    public Greeter(KahaGameCore.Main.Core core, KahaGameCore.Common.IJsonReader deserializer)
    {
        m_core = core;
        m_core.GameDataManager.Load<TestData>(deserializer.Read<TestData[]>(testJson));
        UnityEngine.Debug.Log("Greeter");
    }

    public void Initialize()
    {
        UnityEngine.Debug.Log(m_core.GameDataManager.GetGameData<TestData>(1).TestStringField);
    }
}