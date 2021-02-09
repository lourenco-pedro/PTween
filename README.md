# PTween

<p align="center"> 
<img src="https://media.giphy.com/media/z088g5dDX5IqSsnXM3/giphy.gif" style="max-height: 300px;">
</p>

An easy way to create animations for your UIs. Create dynamics navigation between interfaces, with a simple and intuitive API.

If you guys have any feedbacks or suggestions about the code, please, send them at
pedropereralourenco@gmail.com

## Setup

After downloading the library, import the PTween folder into your Unity project.
All codes that use the PTween API most have the <i>PTween namespace</i> in them.

```cs
using namespace PTween;
```

## Structure

You will need only these three scripts:
  1. <b>PTweenPlayerComponent -</b> Root component that holds all tweeners that will be played.
  2. <b>PTweenComponent -</b> UI Element that will be animated, it holds the configuration of its animation, you can config its position, rotation, scale, and alpha animations. 
  3. <b>PTweenUtil -</b> Static class that has all needed commands for the PTween System.
  
## Creating the UI
  
To create a UI in Unity that uses PTween will be necessary to add a <i>PTweenPlayerComponent</i> in a root panel. There will have all children that use <i>PTweenComponents</i> to be animated.
 
 Eg:
 
 Panel: MainMenu -> <b>PtweenPlayerComponent</b> <br>
 &nbsp; &nbsp;  | <br>
 &nbsp; &nbsp;  |- Title (Text) <b>PtweenComponent</b> <br>
 &nbsp; &nbsp;  | <br>
 &nbsp; &nbsp;  |- Start new Game (Button) <b>PtweenComponent</b> <br>
 &nbsp; &nbsp;  | <br>
 &nbsp; &nbsp;  |- Configuration (Button) <b>PtweenComponent</b> <br>
 &nbsp; &nbsp;  | <br>
 &nbsp; &nbsp;  |- Quit (Button) <b>PtweenComponent</b> <br>
                
 After creating your panel, the PTweenPlayerComponent will try to get all PTweenComponents in its children.
 Each PTweenPlayerComponent will be an animation that can be played on your UI, that animation can also be played backward.
 
 ## Playing the animation
 
 Para tocar um PtweenPlayerComponent, basta usar o comando <i>StartPTweenPlayerComponent</i> em <i>PTweenUtil</i>.
 To play the PTweenPlayerComponent, you just need to use the <i>StartPTweenPlayerComponent</i> command, in <i>PTweenUtil</i>.
 
 ```cs
public static PTweenPlayerInstance StartPTweenPlayerComponent(PTweenPlayerComponent playerComponent, PTweenAnimationDirection animationDirection);
```
 
 Eg:
 
```cs

public PTweenPlayerComponent _fadeMainMenu;
PTweenPlayerInstance _instance;

void Start()
{
    instance = PTweenUtil.StartPTweenPlayerComponent(_fadeMainMenu, PTweenAnimationDirection.ANIMATE_FORWARD);
}

void Update()
{
    if(instance.IsPlayerFinished)
    { ... }
}
```

<b>NOTE:</b> When playing a <i>PTweenPlayerComponent</i>, a <i>PTweenPlayerInstance</i> will be instantiate and added into a list inside <i>PTweenUtil</i>, so it animation can be updated. To update that list, just call for the <i>Update()</i> also in <i>PTweenUtil</i>.

Eg:

```cs
void Start()
{
    //Adding a instance of that PTweenPlayerAnimation and adding it into a list, so it can be updated at PTweenUtil.Update()
    instance = PTweenUtil.StartPTweenPlayerComponent(_fadeMainMenu, PTweenAnimationDirection.ANIMATE_FORWARD);
}
void Update()
{
    if(instance.IsPlayerFinished)
    { ... }
    
    //Updating all Instances added during the runtime
    PTweenUtil.Update();
}
```

# License 

PTween is a free software; you can redistribute it and/or modify it under the terms of the MIT license. See LICENSE for details.
