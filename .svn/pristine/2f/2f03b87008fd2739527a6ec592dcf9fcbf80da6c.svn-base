using System;
using System.Net;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SimpleMvvmToolkit;
using System.Threading;

namespace SimpleMvvmToolkitTest
{
    [TestClass]
    public class MessagingCleanupTests
    {
        [TestMethod]
        public void TestRepeatSend()
        {
            // Create sender and receiver
            SenderViewModel sender = new SenderViewModel();
            ReceiverViewModel receiver = new ReceiverViewModel();

            // Register receiver to get messages
            receiver.Subscribe();

            // Set init value
            int val = new Random().Next(100);

            // Iterate 3 times
            for (int i = 0; i < 3; i++)
            {
                // Set message content
                sender.SentData = val;

                // Send message
                sender.SendOneWayMessage();

                // Assert message was received
                Assert.AreEqual<int>(sender.SentData, receiver.ReceivedData);

                // Increment value
                val += 100;
            }
        }

        [TestMethod]
        public void TestReceiverCleanup()
        {
            // Create sender and receiver
            SenderViewModel sender = new SenderViewModel();
            ReceiverViewModel receiver = new ReceiverViewModel();

            // Register receiver to get messages
            receiver.Subscribe();

            // Set message content
            sender.SentMessage = "hello";

            // Send message
            sender.SendSimpleMessage();

            // Assert message was received
            Assert.AreEqual<string>(sender.SentMessage, receiver.ReceivedMessage);

            // Try to GC reciever
            WeakReference weakReceiver = new WeakReference(receiver);
            receiver = null;
            GC.Collect();
            Thread.Sleep(1000);

            // Assert the receiver was allowed to be GC'd
            Assert.IsFalse(weakReceiver.IsAlive, "Receiver has not been GC'd");

            // Send another message
            sender.SentMessage = "hello again";
            sender.SendSimpleMessage();

            // Assert the receiver is still not alive
            Assert.IsFalse(weakReceiver.IsAlive, "Receiver has not been GC'd after sending message");
        }

        [TestMethod]
        public void TestReceiverUnregister()
        {
            // Create sender and receiver
            SenderViewModel sender = new SenderViewModel();
            ReceiverViewModel receiver = new ReceiverViewModel();

            // Register receiver to get messages
            receiver.Subscribe();

            // Set message content
            sender.SentData = new Random().Next(100);

            // Send message
            sender.SendOneWayMessage();

            // Assert message was received
            Assert.AreEqual<int>(sender.SentData, receiver.ReceivedData);

            // Unregister receiver
            receiver.Unsubscribe();

            // Send another message
            sender.SentData += 100;
            sender.SendOneWayMessage();

            // Assert message was NOT received
            Assert.AreNotEqual<int>(sender.SentData, receiver.ReceivedData);
        }
    }
}