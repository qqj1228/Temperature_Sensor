USE master
GO
CREATE DATABASE TemperSensor
GO
USE TemperSensor
GO

-- 测温过程库
IF OBJECT_ID(N'TemperSensor.dbo.TemperData') IS NOT NULL
    DROP TABLE TemperSensor.dbo.TemperData
GO
CREATE TABLE TemperSensor.dbo.TemperData(
    ID int IDENTITY PRIMARY KEY NOT NULL, -- ID, 自增, 主键
    -- 检测编号， 共17位：温度点编号（3位）+ 检测开始日期（8位，yyyyMMdd）+ 检测开始时间（6位，HHmmss）
    -- 温度点编号取Wifi串口服务器的IP地址最后一位十进制值
    SN varchar(20) NOT NULL,
    VIN varchar(17) NOT NULL, -- 车辆VIN号
    Time float NOT NULL, -- 检测时间，单位：秒
    Temper1 float, -- 探头1温度值，单位：℃
    Temper2 float, -- 探头2温度值，单位：℃
)
GO

-- 测温结果库
IF OBJECT_ID(N'TemperSensor.dbo.TemperResult') IS NOT NULL
    DROP TABLE TemperSensor.dbo.TemperResult
GO
CREATE TABLE TemperSensor.dbo.TemperResult(
    ID int IDENTITY PRIMARY KEY NOT NULL, -- ID, 自增, 主键
    -- 检测编号， 共17位：温度点编号（3位）+ 检测开始日期（8位，yyyyMMdd）+ 检测开始时间（6位，HHmmss）
    -- 温度点编号取Wifi串口服务器的IP地址最后一位十进制值
    SN varchar(20) NOT NULL,
    VIN varchar(17) NOT NULL, -- 车辆VIN号
    Result int NOT NULL, -- 检测结果，1 - 成功，0 - 失败
)
GO