# PTween

<p align="center"> 
<img src="https://media.giphy.com/media/z088g5dDX5IqSsnXM3/giphy.gif" style="max-height: 300px;">
</p>

Uma maneira mais fácil de criar animações para UI. Crie navegações mais dinâmicas entre interfaces, com uma API simples e fácil de entender.

Se vocês tiverem qualquer dica ou feedback em relação ao código, por favor, enviar para
pedropereralourenco@gmail.com

## Setup

Depois de ter baixado a Library, importe a pasta PTween para o seu projeto Unity.

Após importar a lib, todos sos seus códigos que forem precisar de utilizar algum script to Ptween terão que usar a namespace <i>Ptween</i>
```cs
using namespace PTween;
```

## Structure

Você precisará somente de três scripts: 
  1. <b>PTweenPlayerComponent -</b> Component pai que irá conter todos os Tweeners de sua interface que serão tocadas.
  2. <b>PTweenComponent -</b> Component que irá animar o elemento da interface. Seu parent não precisa ter um PTweenPlayerComponent, desde que seu root parent tenha um.   
  3. <b>PTweenUtil -</b> Classe estática que contém todos os comandos necessários para o funcionamento do sistema. 
  
## Creating the UI
  
Para criar uma UI na Unity que use o PTween é recomendável adicionar um <i>PTweenPlayerComponent</i> em um painel, onde nele irá conter em seus childs todos os <i>PTweenComponents</i> que serão animados.
 
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
                
 Depois de criar seu Painel, o PTweenPlayerComponent automaticamente vai tentar pegar todos os TweenComponents presentes em seus Childs. 
 Cada PTweenPlayerComponent vai ser uma animação que pode ser tocada na sua UI, animação essa que pode ser tocada de tras pra frente também.
 
 ## Playing the animation
 
 Para tocar um PtweenPlayerComponent, basta usar o comando <i>StartPTweenPlayerComponent</i> em <i>PTweenUtil</i>.
 
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

<b>NOTE:</b> Ao tocar um <i>PTweenPlayerComponent</i> usando o <i>StartPTweenPlayerComponent</i>, uma <i>PTweenPlayerInstance</i> é adicionada em uma lista em <i>PTweenUtil</i> para que possa ter a sua animação atualizada. Então, para que todas as Tweens rode corretamente, é preciso chamar a função PTweenUtil.Update(), considerando o fato de você ter uma UIManager responsável por controlar toda UI do jogo.<br>

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
