@echo off
:start
echo Enter table name (eg. Customer):
set /p dto=
echo Creating Dto: 
echo %dto%

echo Creating:


SET location=KTB.DNet.Domain\
if Not exist %location%%dto%.vb (
    echo %location%%dto%.vb
    echo ' Your Code > %location%%dto%.vb
) else (
    echo File Exist %location%%dto%Mapper.vb
)

SET location=KTB.DNet.DataMapper\
if Not exist %location%%dto%Mapper.vb (
    echo %location%%dto%Mapper.vb
    echo ' Your Code > %location%%dto%Mapper.vb
) else (
    echo File Exist %location%%dto%Dto.vb
)

SET location=KTB.DNET.InterfaceModel\
if Not exist %location%%dto%Dto.cs (
    echo %location%%dto%Dto.cs
    echo // Your Code > %location%%dto%Dto.cs
) else (
    echo File Exist %location%%dto%Dto.cs
)

SET location=KTB.DNET.InterfaceModel\Filter\
if Not exist %location%%dto%FilterDto.cs (
    echo %location%%dto%FilterDto.cs
    echo // Your Code > %location%%dto%FilterDto.cs
) else (
    echo File Exist %location%%dto%FilterDto.c
)

SET location=KTB.DNET.InterfaceModel\Parameters\
if Not exist %location%%dto%ParameterDto.cs (
    echo %location%%dto%ParameterDto.cs
    echo // Your Code > %location%%dto%ParameterDto.cs
) else (
    echo File Exist %location%%dto%ParameterDto.cs
)


SET location=KTB.DNET.BusinessLogic\
if Not exist %location%%dto%BL.cs (
    echo %location%%dto%BL.cs
    echo // Your Code > %location%%dto%BL.cs
) else (
    echo File Exist %location%%dto%BL.cs
)

SET location=KTB.DNET.BusinessLogic\Interfaces\
if Not exist %location%I%dto%BL.cs (
    echo %location%I%dto%BL.cs
    echo // Your Code > %location%I%dto%BL.cs
) else (
    echo File Exist %location%I%dto%BL.cs
)

SET location=KTB.DNET.BusinessLogic\MapperBL\Profiles\
if Not exist %location%%dto%Profile.cs (
    echo %location%%dto%Profile.cs
    echo // Your Code > %location%%dto%Profile.cs
) else (
    echo File Exist %location%%dto%Profile.cs
)

SET location=KTB.DNet.Interface.WebApi\Controllers\
if Not exist %location%%dto%Controller.cs (
    echo %location%%dto%Controller.cs
    echo // Your Code > %location%%dto%Controller.cs
) else (
    echo File Exist %location%%dto%Controller.cs
)

echo ================================================
echo =====================Finish=====================
echo ================================================

goto start 

#echo Would you like to start again?(Y/N)
#set /p INPUT=
#If /I "%INPUT%"=="y" goto start 
#If /I "%INPUT%"=="n" exit