@echo off
:start
echo __________________________________
echo         Migrate API to KTB       
echo __________________________________
echo Enter table name (eg. Customer):
set /p dto=
echo Enter api type (eg. Masters):
set /p apitype=

echo Copying Dto: 
echo %dto%
SET ktb="..\KTB Production-Develop-DMS\"
echo Copying:


SET location=KTB.DNet.Domain\
if exist %location%%dto%.vb (
    copy /b/v/y %location%%dto%.vb %ktb%%location%%dto%.vb 
    echo File %dto%.vb has been copied
) else (
	if exist %location%VWI_%dto%.vb (
		copy /b/v/y %location%VWI_%dto%.vb %ktb%%location%VWI_%dto%.vb 
		echo File VWI_%dto%.vb has been copied
	) 
)

SET location=KTB.DNet.DataMapper\
if exist %location%%dto%Mapper.vb (
    copy /b/v/y %location%%dto%Mapper.vb %ktb%%location%%dto%Mapper.vb 
    echo File %dto%Mapper.vb has been copied
) else (
	if exist %location%VWI_%dto%Mapper.vb (
		copy /b/v/y %location%VWI_%dto%Mapper.vb %ktb%%location%VWI_%dto%Mapper.vb 
		echo File VWI_%dto%Mapper.vb has been copied
	) 
)

SET location=KTB.DNet.Interface.Model\Dto\%apitype%\
if exist %location%%dto%Dto.cs (
    copy /b/v/y %location%%dto%Dto.cs %ktb%%location%%dto%Dto.cs 
    echo File %dto%Dto.cs has been copied
) else (
    echo File %dto%Dto.cs is not exist 
)

SET location=KTB.DNet.Interface.Model\Filter\%apitype%\
if exist %location%%dto%FilterDto.cs (
    copy /b/v/y %location%%dto%FilterDto.cs %ktb%%location%%dto%FilterDto.cs
    echo File %dto%FilterDto.cs has been copied
) else (
    echo File %dto%FilterDto.cs is not exist
)

SET location=KTB.DNet.Interface.Model\Parameters\%apitype%\
if exist %location%%dto%ParameterDto.cs (
    copy /b/v/y %location%%dto%ParameterDto.cs %ktb%%location%%dto%ParameterDto.cs
    echo File %dto%ParameterDto.cs has been copied
) else (
    echo File %location%%dto%ParameterDto.cs is not exist
)


SET location=KTB.DNET.BusinessLogic\%apitype%\
if exist %location%%dto%BL.cs (
    copy /b/v/y %location%%dto%BL.cs %ktb%%location%%dto%BL.cs
    echo File %dto%BL.cs has been copied
) else (
    echo File %dto%BL.cs is not exist 
)

SET location=KTB.DNet.BusinessLogic\Interfaces\%apitype%\
if exist %location%I%dto%BL.cs (
    copy /b/v/y %location%I%dto%BL.cs %ktb%%location%I%dto%BL.cs
    echo File I%dto%BL.cs has been copied
) else (
    echo File I%dto%BL.cs is not exist
)

SET location=KTB.DNet.BusinessLogic\MapperBL\Profiles\%apitype%\
if exist %location%%dto%Profile.cs (
    copy /b/v/y %location%%dto%Profile.cs %ktb%%location%%dto%Profile.cs
    echo File %dto%Profile.cs has been copied
) else (
    echo File %dto%Profile.cs is not exist 
)

SET location=KTB.DNet.Interface.WebApi\Controllers\%apitype%\
if exist %location%%dto%Controller.cs (
    copy /b/v/y %location%%dto%Controller.cs %ktb%%location%%dto%Controller.cs
    echo File %dto%Controller.cs has been copied
) else (
    echo File %dto%Controller.cs is not exist 
)

SET location=KTB.DNet.Interface.WebApi\Models\Examples\%apitype%\
if exist %location%APICreate%dto%Example.cs (
    copy /b/v/y %location%APICreate%dto%Example.cs %ktb%%location%APICreate%dto%Example.cs
    echo File APICreate%dto%Example.cs has been copied
) else (
    echo File APICreate%dto%Example.cs is not exist 
)

SET location=KTB.DNet.Interface.WebApi\Models\Examples\%apitype%\
if exist %location%APIUpdate%dto%Example.cs (
    copy /b/v/y %location%APIUpdate%dto%Example.cs %ktb%%location%APIUpdate%dto%Example.cs
    echo File APIUpdate%dto%Example.cs has been copied
) else (
    echo File APIUpdate%dto%Example.cs is not exist 
)

SET location=KTB.DNet.Interface.WebApi\Parameters\%apitype%\
if exist %location%%dto%UpdateParameterDto.cs (
    copy /b/v/y %location%%dto%UpdateParameterDto.cs %ktb%%location%%dto%UpdateParameterDto.cs
    echo File %dto%UpdateParameterDto.cs has been copied
) else (
    echo File %dto%UpdateParameterDto.cs is not exist 
)

echo ================================================
echo =====================Finish=====================
echo ================================================

goto start 

#echo Would you like to start again?(Y/N)
#set /p INPUT=
#If /I "%INPUT%"=="y" goto start 
#If /I "%INPUT%"=="n" exit