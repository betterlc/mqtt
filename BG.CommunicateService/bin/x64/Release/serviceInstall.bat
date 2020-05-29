REM 声明采用UTF-8编码
chcp 65001
@echo.服务启动......
@sc create BG.CommunicateService binPath= "%cd%\BG.CommunicateService.exe"
@echo %binPath%
@net start BG.CommunicateService 
@sc config BG.CommunicateService  start= AUTO
@sc description BG.CommunicateService "MQTT服务"
@echo off
@echo.启动完成!
@pause
