﻿using System.Collections.Concurrent;

namespace BackgroundWorksRunner.WorksRunner.Internal;

internal class BackgroundTaskToRunQueue
{
    private ConcurrentQueue<BackgroundTaskToRun> _backgroundTasksToRun = new();

    public void Add(string name, Func<IBackgroundTaskContext> contextFactory, int startDelay, int? repeatInterval, BackgroundTaskExecutionInfo.ChangesWatcher changesWatcher)
    {
        BackgroundTaskToRun btToRun = new(
            name, 
            contextFactory, 
            startTime: DateTime.Now.AddMilliseconds(startDelay), 
            repeatInterval, 
            changesWatcher);

        _backgroundTasksToRun.Enqueue(btToRun);
    }

    public void ExtractAll(Action<BackgroundTaskToRun> action)
    {
        if (!_backgroundTasksToRun.IsEmpty)
        {
            while (_backgroundTasksToRun.TryDequeue(out var btToRun))
            {
                action.Invoke(btToRun);
            }
        }
    }
}