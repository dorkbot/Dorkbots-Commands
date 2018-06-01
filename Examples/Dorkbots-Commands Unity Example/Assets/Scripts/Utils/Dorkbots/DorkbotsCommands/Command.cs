﻿/*
* Author: Dayvid jones
* http://www.dayvid.com
* Copyright (c) Superhero Robot 2018
* http://www.superherorobot.com
* Manged by Dorkbots
* http://www.dorkbots.com/
* Version: 1
* 
* Licence Agreement
*
* You may distribute and modify this class freely, provided that you leave this header intact,
* and add appropriate headers to indicate your changes. Credit is appreciated in applications
* that use this code, but is not required.
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/
using System;

namespace Dorkbots.DorkbotsCommands
{
    public abstract class Command : ICommand
    {
        protected ICommandCallback commandCallback;

        public bool running { get; private set; }
        public string name { get; private set; }
        public ICommandData data{ get; private set; }

        public Command()
        {

        }

        /// <summary>
        /// Initializes the command</summary>
        /// <param name="data">This is of type ICommandData for efficiency, use this param to pass data and references to the command. The param has to implement the interface.</param>
        /// <param name="name">Optional string name that can be used for finding the command or debugging, etc.</param>
        /// <returns>Returns a reference to this Command.</returns>
        public ICommand Init(ICommandData data = null, string name = "")
        {
            this.data = data;
            this.name = name;

            InitVirtual();

            return this;
        }

        protected virtual void InitVirtual()
        {
        }

        public void AddCallback(ICommandCallback commands)
        {
            this.commandCallback = commands;
        }

        public void RemoveCallback(ICommandCallback commands)
        {
            if (this.commandCallback == commands)
            {
                this.commandCallback = null;
            }
            else
            {
                throw new Exception("A ICommandHandler that doen't contain this ICommand is attempting to remove it!!!!!!");
            }
        }

        public void Execute()
        {
            running = true;

            ExecuteVirtual();
        }

        protected abstract void ExecuteVirtual();

        public void Stop(bool complete = false)
        {
            if (running)
            {
                running = false;

                StopVirtual(complete);

                if (complete) this.Complete();
            }
        }

        protected virtual void StopVirtual(bool complete = false)
        {

        }

        protected void Complete()
        {
            commandCallback.CommandCompleted(this);
        }

        public void Dispose()
        {
            DisposeVirtual();

            if (running) Stop();
            commandCallback = null;
            data = null;
        }

        protected virtual void DisposeVirtual()
        {

        }
    }
}