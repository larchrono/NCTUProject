using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoraLib {
    public abstract class SingletonMonoBase : MonoBehaviour {
        protected internal virtual void OnSingletonAwake () { }
        protected internal virtual void OnSingletonDestroy () { }
    }
}