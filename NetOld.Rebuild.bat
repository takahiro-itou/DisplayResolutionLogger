
setlocal
set  script_dir=%~dp0

CALL  "%script_dir%Config\Common.cnf.bat"
CALL  "%script_dir%Config\ResGen.bat"

set  target=Rebuild


msbuild  -restore  -t:Clean     ^
    -p:Configuration=%config%   -p:Platform=x86     ^
    "%solution%.NetOld.sln"

msbuild  -restore  -t:%target%  ^
    -p:Configuration=%config%   -p:Platform=x86     ^
    "%solution%.NetOld.sln"

endlocal
