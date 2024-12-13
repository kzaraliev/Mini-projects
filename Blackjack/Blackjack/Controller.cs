using Blackjack.Entities;
using Blackjack.Utilities;

namespace Blackjack;

public class Controller
{
    public Controller()
    {
        area = new Area();
        player = new Player();
        computer = new Computer();
        shared = new Shared();
    }

    private Area area;
    private Player player;
    private Computer computer;
    private Shared shared;
    private bool isAreaAlreadyExist = false;
    private bool isGameOver = false;

    public void Run()
    {
        while (true)
        {
            player = new Player();
            computer = new Computer();
            if (!isAreaAlreadyExist)
            {
                area.DrawArea();
            }

            if (isAreaAlreadyExist)
            {
                area.ClearOldInformation();
            }

            Thread.Sleep(500);

            //here is cards drawing of comp and player
            computer.TakeFirstCard();
            player.TakeFirstCards();

            area.DrawPoints(computer.Points, player.Points);

            isGameOver = PointsChecker(false);

            if (!isGameOver)
            {
                bool isPlayerStaying = true;
                while (isPlayerStaying)
                {
                    isPlayerStaying = player.PlayTurn();
                    if (!isPlayerStaying)
                    {
                        break;
                    }
                    if (PointsChecker(false))
                    {
                        isGameOver = true;
                        break;
                    }
                }
            }

            if (!isGameOver)
            {
                bool isComputerStaying = true;
                while (isComputerStaying)
                {
                    isComputerStaying = computer.PlayTurn(player.Points);
                    if (!isComputerStaying)
                    {
                        break;
                    }
                    if (PointsChecker(false))
                    {
                        isGameOver = true;
                        break;
                    }

                    //Different delay on drawing computer's cards
                    Thread.Sleep(new Random().Next(300, 500));
                }
            }

            if (!isGameOver)
            {
                PointsChecker(true);
            }

            isAreaAlreadyExist = true;
            if (!DoesGameContinue())
            {
                shared.FinalMessage();
                return;
            }
        }
    }

    private bool DoesGameContinue()
    {
        string key = shared.AnimationForReplayOrLeave(Constrants.ReplayOrEscape);

        if (key == "R")
        {
            return true;
        }

        return false;
    }

    private bool PointsChecker(bool finalCheck)
    {
        bool flag = false;
        if (player.Points == Constrants.maxPoints)
        {
            flag = area.DisplayMessage(Constrants.blackjack, null, Constrants.playerWins);
        }
        else if (computer.Points == Constrants.maxPoints)
        {
            flag = area.DisplayMessage(Constrants.blackjack, null, Constrants.compWins);
        }

        if (player.Points > Constrants.maxPoints)
        {
            flag = area.DisplayMessage(null, Constrants.busted, Constrants.compWins);
        }
        else if (computer.Points > Constrants.maxPoints)
        {
            flag = area.DisplayMessage(null, Constrants.busted, Constrants.playerWins);
        }

        if (finalCheck)
        {
            if (player.Points > computer.Points)
            {
                flag = area.DisplayMessage(null, null, Constrants.playerWins);
            }
            else if (computer.Points > player.Points)
            {
                flag = area.DisplayMessage(null, null, Constrants.compWins);
            }
        }

        return flag;
    }
}