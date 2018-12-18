#!/usr/bin/env sh

echo "======================================="
echo "= JR Cms .NET ! assembly ="
echo "======================================="

echo scanning assemblies...

cur=$(pwd)

cd ../bin

echo "1. build dll "

#set megdir=%dir%\dist\bin\
#if exist "%cur%/merge.exe" (
#cd %dir%/src/bin/
#echo  /keyfile:%dir%\src\JR.cms.snk>nul

${cur}/merge.exe -closed -ndebug \
	/keyfile:../cms/jr.cms.snk \
	/targetplatform:v4 /target:dll /out:../dist/jrcms.dll \
 	JR.Cms.Core.dll JR.Cms.BLL.dll JR.Cms.DAL.dll \
	JR.Cms.Domain.Interface.dll JR.Cms.CacheService.dll \
	JR.Cms.DataTransfer.dll JR.Cms.Domain.Implement.Content.dll \
 	JR.Cms.DB.dll JR.Cms.Cache.dll JR.Cms.Domain.Implement.Site.dll \
 	JR.Cms.Domain.Implement.User.dll JR.Cms.Infrastructure.dll \
 	JR.Cms.Service.dll JR.Cms.ServiceContract.dll \
 	JR.Cms.ServiceUtil.dll JR.Cms.ServiceRepository.dll JR.Cms.IDAL.dll \
 	JR.Cms.Sql.dll JR.Cms.Utility.dll JR.Cms.WebImpl.dll \
	&& cd ..

echo "2. prepare files"
tmp_dir=$(pwd)/dist/tmp
dll_dir=$(pwd)/refrence.dll
exe_dir=$(pwd)/script

rm -rf $tmp_dir && mkdir -p $tmp_dir/templates \
	&& mkdir -p $tmp_dir/bin \
	&& mkdir -p $tmp_dir/../patch

cd $(find . -path "*/JR.Cms.WebUI") && \
	cp -r \$server install plugins public \
	Global.asax Web.config $tmp_dir &&\
	cp -r templates/default $tmp_dir/templates/ &&\
        cd - > /dev/null

cp LICENSE README.md $tmp_dir && cp dist/boot.dll \
	$dll_dir/StructureMap.dll \
	$dll_dir/System.Data.SQLite.dll $tmp_dir/bin

cp dist/jrcms.dll $dll_dir/jrdev* $tmp_dir/public/assemblies 

echo "3. package upgrade zip"
cd $tmp_dir && cp -r $(find $exe_dir/../cms -name "upgrade.xml") ../patch \
	&& $exe_dir/7z.exe a -tzip ../patch/boot.zip bin >/dev/null \
	&& $exe_dir/7z.exe a -tzip ../patch/cms-patch.zip >/dev/null

echo "4. package all" 
$exe_dir/7z.exe a -tzip ../jrcms-dist-1.0.zip * >/dev/null

echo "5. clean" && rm -rf $tmp_dir

sleep 2 && echo "build success!"