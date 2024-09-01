#include <WiFi.h>
#include "PubSubClient.h"

// WiFi
const char *ssid = "ERLAN_PC 6986";
const char *password = "5;2D5o91";

// MQTT Server
const char *mqtt_server = "192.168.18.226";
const int mqtt_port = 1883;

WiFiClient wifiClient;
PubSubClient mqttClient(wifiClient);

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);

  WiFi.begin(ssid, password);
  while(WiFi.status() != WL_CONNECTED){
    delay(500);
    Serial.println("Connecting to Wifi ...");
  }

  mqttClient.setServer(mqtt_server, mqtt_port);
  while (!mqttClient.connected()) {
    String clientId = "esp32";
    clientId += String(WiFi.macAddress());

    if (mqttClient.connect(clientId.c_str())) {
      Serial.println("Public EMQX MQTT broker connected");
    } else {
      Serial.print("failed with state ");
      Serial.print(mqttClient.state());
      delay(2000);
    }

  }
}

void loop() {
  // put your main code here, to run repeatedly:

}
