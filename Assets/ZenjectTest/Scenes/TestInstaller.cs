using Zenject;
using UnityEngine;
using System.Collections;

public class TestInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log("InstallBindings");
        Container.Bind<string>().FromInstance("Hello World!");
        Container.Bind<Greeter>().AsSingle();
        Container.Bind<Tester>().AsTransient();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Container.Resolve<Tester>().Call();
        }
    }
}

public class Greeter
{
    public Greeter(string message)
    {
        Debug.Log(message);
    }
}

public class Tester
{
    [Inject]
    public Greeter TestGreeter { get; private set; }

    public void Call()
    {
        Debug.Log("TestGreeter.GetHashCode()=" + TestGreeter.GetHashCode());
        Debug.Log("GetHashCode()=" + GetHashCode());//
    }
}