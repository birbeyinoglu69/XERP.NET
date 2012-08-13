using System;
using System.Net;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SimpleMvvmToolkit;

namespace SimpleMvvmToolkitTest
{
    [TestClass]
    public class MessagingTests
    {
        [TestMethod]
        public void TestSimpleMessage()
        {
            // Create sender and receiver
            SenderViewModel sender = new SenderViewModel();
            ReceiverViewModel receiver = new ReceiverViewModel();

            // Register receiver to get messages
            receiver.Subscribe();

            // Set message content
            sender.SentMessage = "hello";

            // Capture orig values
            int origReceiverData = receiver.ReceivedData;
            bool origSenderData = sender.ReceivedData;

            // Send message
            sender.SendSimpleMessage();

            // Assert message was received
            Assert.AreEqual<string>(sender.SentMessage, receiver.ReceivedMessage);

            // Assert receiver and sender data have not changed
            Assert.AreEqual<int>(origReceiverData, receiver.ReceivedData);
            Assert.AreEqual<bool>(origSenderData, sender.ReceivedData);
        }

        [TestMethod]
        public void TestOneWayMessage()
        {
            // Create sender and receiver
            SenderViewModel sender = new SenderViewModel();
            ReceiverViewModel receiver = new ReceiverViewModel();

            // Register receiver to get messages
            receiver.Subscribe();

            // Set message content
            sender.SentData = new Random().Next(100);

            // Capture orig values
            string origReceiverMessage = receiver.ReceivedMessage;
            bool origSenderData = sender.ReceivedData;

            // Send message
            sender.SendOneWayMessage();

            // Assert message was received
            Assert.AreEqual<int>(sender.SentData, receiver.ReceivedData);

            // Assert receiver and sender data have not changed
            Assert.AreEqual<string>(origReceiverMessage, receiver.ReceivedMessage);
            Assert.AreEqual<bool>(origSenderData, sender.ReceivedData);
        }

        [TestMethod]
        public void TestTwoWayMessage()
        {
            // Create sender and receiver
            SenderViewModel sender = new SenderViewModel();
            ReceiverViewModel receiver = new ReceiverViewModel();

            // Register receiver to get messages
            receiver.Subscribe();

            // Capture orig values
            string origReceiverMessage = receiver.ReceivedMessage;
            int origReceiverData = receiver.ReceivedData;

            // Set message content
            sender.SentData = new Random().Next(100);
            receiver.SentData = true;

            // Send message
            sender.SendTwoWayMessage();

            // Assert data was received
            Assert.AreEqual<int>(sender.SentData, receiver.ReceivedData);

            // Assert data was sent
            Assert.AreEqual<bool>(receiver.SentData, sender.ReceivedData);

            // Assert receiver and sender data have not changed
            Assert.AreEqual<string>(origReceiverMessage, receiver.ReceivedMessage);
        }
    }
}