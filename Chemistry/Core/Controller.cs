using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarRacing.Core.Contracts;
using Chemistry.Core.Models;
using Chemistry.IO;
using Chemistry.Utilities.Messages;

namespace Chemistry.Core;

public class Controller : IController
{
    private string language;

    public Controller()
    { }

    public void Control(string inputPart)
    {
        switch (inputPart)
        {
            case nameof(Intro):
                Intro intro = new Intro();
               language = intro.IntroPlay();
                break;

            case nameof(FirstGame):
                FirstGame firstgame = new FirstGame();
                firstgame.FirstGameAlert(language);
                break;

            case nameof(FirstPartPresentation):
                FirstPartPresentation firstPartPres = new FirstPartPresentation();
                firstPartPres.FirstPartPlay(language);
              break;

            case nameof(SecondPartPresentation):
                SecondPartPresentation secondPart = new SecondPartPresentation();
                secondPart.SecondPartPlay(language);
                break; }
    }
}