REM 声明采用UTF-8编码
chcp 65001
@echo.服务关闭中...
@echo off
@net stop BG.CommunicateService
@echo off
@echo.关闭结束
@echo.服务删除中...
@echo off
@sc delete BG.CommunicateService
@echo off
@echo.删除结束
@pause