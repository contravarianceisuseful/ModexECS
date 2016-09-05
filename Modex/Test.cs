using UnityEngine;
using System.Collections;
using ModexECS;

public class Test : MonoBehaviour
{
    public Modex Modex;
	// Use this for initialization
	void Start () {

        Modex = new Modex();
#if false //core test
        Modex.StartSystem<TestAddSystem>();
	    Modex.StartSystem<TestRemoveSystem>();
	    Modex.StartSystem<TestModifySystem>();
	    var entity = Modex.CreateEntity();

	    entity.AddComponent(new TestComponent() {TestMessage = "the message has been sent"});
	    entity.ModifyComponent(new TestComponent() {TestMessage = "Modified message"});
	    entity.ModifyComponent<TestComponent, string>("TestMessage", "will this actually work?");
	    entity.ModifyComponent<TestComponent>("test");
	    entity.RemoveComponent<TestComponent>();
#endif
#if true //propery modification test
	    Modex.StartSystem<TestModifySystem>();
	    var entity = Modex.CreateEntity();
	    entity.AddComponent(new PropertyTestComponent() {propertyMessage = "base message"});
	    entity.ModifyComponent(new PropertyTestComponent() {propertyMessage = "message modified"});
	    entity.ModifyComponent<PropertyTestComponent>("message modified a second time");
	    entity.ModifyComponent<PropertyTestComponent, string>("propertyMessage", "final message modification");
#endif


	}

    // Update is called once per frame
    void Update() {
	
	}


    //-------------------
    //Tests
    //-------------------


    public class TestAddSystem : OnAddSystem<TestComponent>
    {
        public override void OnAdd(Entity entity)
        {
            Debug.Log("Test message is: " + AddedComponent.TestMessage);
        }
    }

    public class TestRemoveSystem : OnRemoveSystem<TestComponent>
    {
        public override void OnRemove(Entity entity)
        {
            Debug.Log(RemovedComponent + " has been removed.");
        }
    }

    public class TestModifySystem : OnModifySystem<TestComponent> {
        public override void OnModify(Entity entity)
        {
            Debug.Log("Modified message is: " + ModifiedComponent.TestMessage);
        }
    }

    public class TestComponent : IModifiableComponent
    {
        public string TestMessage;
    }

    public class PropertyTestComponent : TestComponent
    {
        public string propertyMessage { get; set; }
    }
}
