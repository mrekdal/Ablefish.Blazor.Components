﻿using Ablefish.Blazor.Observer;

namespace Ablefish.Blazor.Status
{
    public interface IStatusMessage: ISubjectMinimal
    {
        bool HasData { get; }
        string Header { get; }
        bool IsEmpty { get; }
        StatusType Status { get; }
        string Text { get; }

        void Clear();
        void SetException(Exception exception);
        void SetInformation(string statusHeader, string statusText);
        void SetWarning(string statusHeader, string statusText);
        void SetSuccess(string statusHeader, string statusText);
        void Update(string statusHeader, string statusText, StatusType statusType);
    }
}