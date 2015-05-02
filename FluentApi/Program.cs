using System;
using System.Collections.Generic;
using System.Threading;

namespace FluentTask
{
    class Behavior
    {
        private readonly List<Action> _actions;

        public Behavior()
        {
            _actions = new List<Action>();
        }

        public void Execute()
        {
            foreach (var action in _actions)
                action();
        }

        public Behavior Say( string sentence)
        {
            _actions.Add(() => Console.WriteLine(sentence));
            return this;
        }

        public Behavior UntilKeyPressed(Action<Behavior> actions)
        {
            _actions.Add(() =>
            {
                while (!Console.KeyAvailable)
                {
                    Thread.Sleep(100);
                    var subBehavior = new Behavior();
                    actions(subBehavior);
                    subBehavior.Execute();
                }
                Console.ReadKey(false);
            });
            return this;
        }

        public Behavior Jump(JumpHeight jumpHeight)
        {
            _actions.Add(
                () =>
                {
                    Console.WriteLine(jumpHeight == JumpHeight.High ?
                        "Прыгнул высоко" : "прыгнул низко");
                });
            return this;
        }

        public Behavior Delay(TimeSpan delay)
        {
            _actions.Add(() => Thread.Sleep(delay));
            return this;
        }
    }

    internal class Program
    {
        private static void Main()
        {
            var behaviour = new Behavior()
                            .Say("Привет мир!")
                            .UntilKeyPressed(b => b
                                .Say("Ля-ля-ля!")
                                .Say("Тру-лю-лю"))
                            .Jump(JumpHeight.High)
                            .UntilKeyPressed(b => b
                                .Say("Aa-a-a-a-aaaaaa!!!")
                                .Say("[набирает воздух в легкие]"))
                            .Say("Ой!")
                            .Delay(TimeSpan.FromSeconds(1))
                            .Say("Кто здесь?!")
                            .Delay(TimeSpan.FromMilliseconds(2000));

            behaviour.Execute();


        }
    }
}