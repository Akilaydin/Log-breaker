using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class NotificationController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        AndroidNotificationCenter.CancelAllNotifications();

        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        var notification = new AndroidNotification();
        notification.Title = "Back to game!";
        notification.Text = "So many logs have to be broken.\nShow them all!";
        notification.FireTime = System.DateTime.Now.AddSeconds(15);

        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }
}