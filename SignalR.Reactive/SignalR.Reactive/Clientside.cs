﻿using System;
using Microsoft.AspNet.SignalR;

namespace SignalR.Reactive
{
    public class Clientside<T>
    {
        private readonly IObservable<T> _observable;
        
        internal Clientside(IObservable<T> observable)
        {
            _observable = observable;
        }
        
        public IDisposable Observable<THub>(string eventName) where THub : Hub, new()
        {
            return Observable<THub>(eventName, null);
        }

        public IDisposable Observable<THub>(string eventName, string clientName) where THub : Hub, new()
        {
            dynamic clients = RxHelper.GetHubClients<THub>();

            clients = string.IsNullOrEmpty(clientName) ? clients : clients.client[clientName];

            return _observable.Subscribe(
                x => RxHelper.RaiseOnNext(eventName, clients, x),
                x => RxHelper.RaiseOnError(eventName, clients, x),
                () => RxHelper.RaiseOnCompleted(eventName, clients)
                );
        }
    }

    public static class SignalRObservableExtensions
    {
        public static Clientside<T> ToClientside<T>(this IObservable<T> observable)
        {
            return new Clientside<T>(observable);
        }
    }
}
