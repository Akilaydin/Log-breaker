using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class NotificationController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        AndroidNotificationCenter.CancelAllNotifications(); //If player enter the game, clear all scheduled notifications and make a new one.

        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        var notification = new AndroidNotification(); //Fire notification every eight hours.
        notification.Title = "Back to game!";
        notification.Text = "So many logs have to be broken.\nShow them all!";
        notification.FireTime = System.DateTime.Now.AddHours(8); 

        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }
}
