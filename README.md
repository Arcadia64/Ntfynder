# Ntfynder for FINAL FANTASY XIV
A Dalamud plugin to ping your phone via [ntfy](https://ntfy.sh) whenever a Party Finder event or duty pop happens with an AFK client.

This is a fork of [PushyFinder](https://github.com/lostkagamine/PushyFinder) that uses ntfy as an alternative to Pushover for sending/receiving notifications.

Credit to [@lostkagamine](https://github.com/lostkagamine/) for the original idea and implementation.

## Usage

### Phone Setup
1. Install the ntfy app.  
[![Google Play](https://docs.ntfy.sh/static/img/badge-googleplay.png)](https://play.google.com/store/apps/details?id=io.heckel.ntfy)
[![F-Droid](https://docs.ntfy.sh/static/img/badge-fdroid.png)](https://f-droid.org/en/packages/io.heckel.ntfy/)
[![App Store](https://docs.ntfy.sh/static/img/badge-appstore.png)](https://apps.apple.com/us/app/ntfy/id1625396347)
2. Once installed, open it and subscribe to a topic of your choosing. Topics don't have to explicitly be created, so just pick a unique enough name.  
***Note that topic names are public, anyone subscribed to this topic will receive the same notifications as you and can push their own notifications. So try to make this unique.***

### In Game
Configure the plugin in game using `/ntfy` command.  
  - Set ***Ntfy topic*** to the topic name you subscribed to in the app. 
  - Can toggle the following options.
      - Send messages for duty pop
      - Ignore AFK status and always notify