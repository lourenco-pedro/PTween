# PTween

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
  
  ## 
