﻿#include <WiFiNINA.h>

//please enter your sensitive data in the Secret tab
char ssid[] = "iPhone";
char pass[] = "Ashish124";
WiFiClient client;

char HOST_NAME[] = "maker.ifttt.com";
String PATH_NAME = "/trigger/SIT_210_3.1P/with/key/irZqO-Xrh6A2jywZ0ZwnVdXa8upVTxg2OZsUfQ9GszP"; // change your EVENT-NAME and YOUR-KEY
String queryString = "?value1=57&value2=25";

void setup()
{
    // initialize WiFi connection
    WiFi.begin(ssid, pass);

    Serial.begin(9600);
    while (!Serial) ;

    // connect to web server on port 80:
    if (client.connect(HOST_NAME, 80))
    {
        // if connected:
        Serial.println("Connected to server");
    }
    else
    {// if not connected:
        Serial.println("connection failed");
    }
}

void loop()
{
    if (Serial.read() == 's')
    {

        // make a HTTP request:
        // send HTTP header
        client.println("GET " + PATH_NAME + queryString + " HTTP/1.1");
        client.println("Host: " + String(HOST_NAME));
        client.println("Connection: close");
        client.println(); // end HTTP header


        while (client.connected())
        {
            if (client.available())
            {
                // read an incoming byte from the server and print it to serial monitor:
                char c = client.read();
                Serial.print(c);
            }
        }

        // the server's disconnected, stop the client:
        client.stop();
        Serial.println();
        Serial.println("disconnected");
    }
}