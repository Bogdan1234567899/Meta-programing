Результати запуску

Позитивний сценарій — все працює нормально, 5 подій оброблено:

=== Positive scenario ===

[LOG] Processing event: fire_alarm
[Email -> Security] [Priority 1] FIRE_ALARM: Fire detected in building A
[SMS -> Security] [Priority 1] FIRE_ALARM: Fire detected in building A
[Console -> Security] [Priority 1] FIRE_ALARM: Fire detected in building A
[LOG] Event fire_alarm routed successfully
[LOG] Processing event: network_outage
[Email -> Admin] [Priority 3] NETWORK_OUTAGE: WiFi down in library
[SMS -> Admin] [Priority 3] NETWORK_OUTAGE: WiFi down in library
[Console -> Admin] [Priority 3] NETWORK_OUTAGE: WiFi down in library
[LOG] Event network_outage routed successfully
[LOG] Processing event: door_forced
[Email -> Security] [Priority 2] DOOR_FORCED: Door forced open in lab 3
[SMS -> Security] [Priority 2] DOOR_FORCED: Door forced open in lab 3
[Console -> Security] [Priority 2] DOOR_FORCED: Door forced open in lab 3
[LOG] Event door_forced routed successfully
[LOG] Processing event: medical_emergency
[Email -> Teacher] [Priority 1] MEDICAL_EMERGENCY: Student fainted in gym
[SMS -> Teacher] [Priority 1] MEDICAL_EMERGENCY: Student fainted in gym
[Console -> Teacher] [Priority 1] MEDICAL_EMERGENCY: Student fainted in gym
[LOG] Event medical_emergency routed successfully
[LOG] Processing event: power_failure
[Email -> Admin] [Priority 1] POWER_FAILURE: Power lost in dormitory B
[SMS -> Admin] [Priority 1] POWER_FAILURE: Power lost in dormitory B
[Console -> Admin] [Priority 1] POWER_FAILURE: Power lost in dormitory B
[LOG] Event power_failure routed successfully

Негативний сценарій — передаємо null і порожній список, програма одразу падає:

=== Negative scenario (Fail-fast) ===

Fail-fast caught: Missing dependency - channels
Fail-fast caught: At least one notification channel is required (Parameter 'channels')
