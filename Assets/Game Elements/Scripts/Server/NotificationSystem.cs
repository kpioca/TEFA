
using System;
using UnityEngine;
using LootLocker.Requests;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using LootLocker.LootLockerStaticStrings;

public class NotificationSystem : MonoBehaviour
{
    public void Initialize()
    {
        // We're only interested in unread notifications for this how to.
        // If you want to see information about notifications you've previously marked as read then set this to true
        bool showRead = false;

        // The key that we saved from invoking a trigger, or calling a purchase to get the correct notification
        string notificationKey = "saved_trigger_key_or_catalog_item_id_from_a_previous_response";

        // You can optionally return only a specific priority
        LootLocker.LootLockerEnums.LootLockerNotificationPriority? priority = null;

        // You can optionally return only a specific type of notification. 
        // Triggers and purchased items are of the type LootLocker.LootLockerStaticStrings.LootLockerNotificationTypes.PullRewardAcquired
        string ofType = "";

        // The source of the notification: a type of purchase or trigger.
        // Use the static strings provided in LootLocker.LootLockerStaticStrings.LootLockerNotificationSources 
        // to specify what type of notification you are requesting.
        // For purchases, use: 
        // LootLocker.LootLockerStaticStrings.LootLockerNotificationSources.Purchasing.(the source of purchase: LootLocker, AppleAppStore, GooglePlayStore, SteamStore)
        // For triggers, use: 
        // LootLocker.LootLockerStaticStrings.LootLockerNotificationSources.Triggers
        string notificationSource = LootLocker.LootLockerStaticStrings.LootLockerNotificationSources.Triggers;

        // The Page and Count parameters are used for pagination.
        // Count means count per page, and Page is what page to access.
        // For example: A count of 100, and page 2 would list all notifications (if any) from 101-201
        int count = 10;
        int page = 1;
        LootLockerSDKManager.ListNotifications(showRead, priority, LootLockerNotificationTypes.PullRewardAcquired, notificationSource, count, page, (response) =>
        {
            if (!response.success)
            {
                Debug.Log("Error, could not list notifications:" + response.errorData.message);
                // Add your own code here to handle the error
                return;
            }

            // A notifications request returns success even if no notifications were found,
            // handle that by checking if the notifications returned are null
            if (response.Notifications == null)
            {
                Debug.LogWarning("Error: no notifications found");
                // Add your own code to handle what should happen
                return;
            }

            // Get the desired notification
            LootLockerNotification[] matchingNotifications;
            bool notificationWasFound = response.TryGetNotificationsByIdentifyingValue(notificationKey, out matchingNotifications);

            if (!notificationWasFound || matchingNotifications?.Length == 0)
            {
                Debug.LogWarning("Error: Notification not found.");
                // Add your own code here to handle the error
                return;
            }

            // In this example we know that the trigger has only been invoked once so we can safely access the first element of the array
            LootLockerNotification desiredNotification = matchingNotifications[0];

            if (desiredNotification == null)
            {
                Debug.LogWarning("Error: Notification not found.");
                // Add your own code here to handle the error
                return;
            }

            // The enum value in the notification.Content.Body.Kind field shows what type of "thing" was rewarded, use that to know which field in the body contains the data about the reward.
            switch (desiredNotification.Content.Body.Kind)
            {
                case LootLocker.LootLockerEnums.LootLockerNotificationContentKind.group:
                    // The reward is a reward group which contains a list of "associations". Ie, a list of assets, currencies, or other similar things rewarded as one in this single reward.
                    // Handle this in line with your game logic 
                    Debug.Log($"Trigger with key '{notificationKey}' gave reward of type group. The group has name '{desiredNotification.Content.Body.Group.Name}', description '{desiredNotification.Content.Body.Group.Description}'. and {desiredNotification.Content.Body.Group.Associations.Length} associations.");
                    break;
                case LootLocker.LootLockerEnums.LootLockerNotificationContentKind.currency:
                    // The reward is a currency. Handle this in line with your game logic 
                    Debug.Log($"Trigger with key '{notificationKey}' gave reward of type currency: {desiredNotification.Content.Body.Currency.Details.Amount} {desiredNotification.Content.Body.Currency.Details.Code}");
                    break;
                case LootLocker.LootLockerEnums.LootLockerNotificationContentKind.asset:
                    // The reward is an asset. Handle this in line with your game logic 
                    Debug.Log($"Trigger with key '{notificationKey}' gave reward of type asset with name '{desiredNotification.Content.Body.Asset.Details.Name}'");
                    break;
                case LootLocker.LootLockerEnums.LootLockerNotificationContentKind.progression_reset:
                    // The reward is a progression reset. Handle this in line with your game logic 
                    Debug.Log($"Trigger with key '{notificationKey}' gave reward of type progression reset which resets the progression named '{desiredNotification.Content.Body.Progression_reset.Details.Name}'");
                    break;
                case LootLocker.LootLockerEnums.LootLockerNotificationContentKind.progression_points:
                    // The reward is progression points. Handle this in line with your game logic 
                    Debug.Log($"Trigger with key '{notificationKey}' gave reward of type progression points which gives the player {desiredNotification.Content.Body.Progression_points.Details.Amount} points in the progression named '{desiredNotification.Content.Body.Progression_points.Details.Name}'");
                    break;
                default:
                    Debug.LogWarning($"Unhandled case {desiredNotification.Content.Body.Kind.ToString()}");
                    break;
            }

            // After you've handled the notification in your game logic, remember to mark it as read.
            // This way it will not be returned on the next request unless you specifically request notifications marked as read.
            desiredNotification.MarkThisNotificationAsRead((response) =>
            {
                if (response.success)
                {
                    Debug.Log("Marked notification as read");
                }
                else
                {
                    Debug.LogWarning("Error marking notification as read: " + response.errorData.message);
                    // Add your own code here to handle the error
                }
            });
        });
    }
}
