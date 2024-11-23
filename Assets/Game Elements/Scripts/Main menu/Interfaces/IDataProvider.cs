using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataProvider
{
    void Save();
    void TryLoad(Action<string> callback);
}
