﻿using BackgroundWorksRunner.WorksRunner;

namespace BackgroundWorksRunner.Workers;

public class WorkRunner2 : IWorkRunner
{
    public static string Name => "Serviço 2";

    public async Task Execute(IWorkRunnerStatus s, CancellationToken cancellationToken)
    {
        Console.WriteLine($"{DateTime.Now:HH:mm:ss} Executando {Name}");

        int max = 400;

        for (int n = 1; n <= max; n++)
        {
            await Task.Delay(50);
            s.UpdateStatusInfo($"{n} de {max}");
        }
    }
}
