using System.Collections;
using System.Collections.Generic;
using System;

public interface IPlayerInput
{
    event Action<float> OnHorizontalUpdate;

    event Action OnSpaceUpdate;
    float Speed { get; }
}

public class BasicInterface
{

}
