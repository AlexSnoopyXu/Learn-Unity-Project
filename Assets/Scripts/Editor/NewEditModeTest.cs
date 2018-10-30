using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class NewEditModeTest {

	[Test]
	public void NewEditModeTestSimplePasses() {
		// Use the Assert class to test conditions.
	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator NewEditModeTestWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}

    [Test]
    public void Test()
    {
        //List<string> a = new List<string>();
        string[] a = new string[1000];
        a[100] = "ssss";
        a[3] = "aaaa";

        foreach(string s in a)
        {
            Debug.LogError(s);
        }

        Debug.LogError("a count : " + a.Length);
    }
}
