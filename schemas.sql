USE master
GO
CREATE DATABASE TemperSensor
GO
USE TemperSensor
GO

-- 用户信息表
IF OBJECT_ID(N'TemperSensor.dbo.TemperUser') IS NOT NULL
    DROP TABLE TemperSensor.dbo.TemperUser
GO
CREATE TABLE TemperSensor.dbo.TemperUser(
    ID int IDENTITY PRIMARY KEY NOT NULL, -- ID, 自增, 主键
    UserName varchar(20) NOT NULL, -- 用户名
    PassWord varchar(32) NOT NULL, -- 密码
)
GO
-- 插入默认密码
INSERT TemperSensor.dbo.TemperUser
    VALUES (
        'admin',
        '81DC9BDB52D04DC20036DBD8313ED055' -- 默认密码1234
    )
GO

-- 测温过程表
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
    TemperSTD float, -- 温度设定值，单位：℃
)
GO

-- 测温结果表
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