using KahaGameCore.EffectCommand;
using System;
using System.Collections.Generic;
using Zenject;

public class MyGameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind(typeof(IInitializable), typeof(ITickable)).To<GreeterCreator>().AsSingle();
        Container.Bind<ITickable>().To<GreeterCaller>().AsSingle();
    }
}

public class GreeterCreator : IInitializable, ITickable
{
    private readonly EffectCommandFactoryContainer m_effectCommandFactoryContainer;
    private readonly EffectProcesser.Facotry m_effectProcesserFacotry;
    private readonly EffectCommandDeserializer m_effectCommandDeserializer;

    public GreeterCreator(EffectCommandFactoryContainer effectCommandFactoryContainer, EffectProcesser.Facotry effectProcesserFacotry, EffectCommandDeserializer effectCommandDeserializer)
    {
        m_effectCommandFactoryContainer = effectCommandFactoryContainer;
        m_effectProcesserFacotry = effectProcesserFacotry;
        m_effectCommandDeserializer = effectCommandDeserializer;
    }

    public void Initialize()
    {
        m_effectCommandFactoryContainer.RegisterFactory("Greet", new GreetEffectCommandFatory());
    }

    private int m_count = 0;
    public void Tick()
    {
        if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.X))
        {
            string _testData = "Stage1 { Greet(Hello-World-" + m_count.ToString() +");  }";

            Dictionary<string, List<EffectProcesser.EffectData>> _deserializedDatas = m_effectCommandDeserializer.Deserialize(_testData);
            EffectProcesser _newEffectProcesser = m_effectProcesserFacotry.Create();
            _newEffectProcesser.SetUp(_deserializedDatas);

            UnityEngine.Debug.Log("Greeter created");
            m_count++;
        }
    }
}

public class GreeterCaller : ITickable
{
    private readonly SignalBus m_signalBus;

    public GreeterCaller(SignalBus signalBus)
    {
        m_signalBus = signalBus;
    }

    public void Tick()
    {
        if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Alpha1))
        {
            m_signalBus.Fire(new EffectTimingTriggedSignal(null, null, "Stage1"));
        }
    }
}

public class GreetEffectCommandFatory : EffectCommandFactoryBase
{
    public override EffectCommandBase Create()
    {
        return new GreetEffectCommand();
    }
}

public class GreetEffectCommand : EffectCommandBase
{
    public override void Process(string[] vars, Action onCompleted, Action onForceQuit)
    {
        UnityEngine.Debug.Log("Greet Msg=" + vars[0]);
    }
}