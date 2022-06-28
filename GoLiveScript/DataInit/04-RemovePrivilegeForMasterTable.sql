update Privilege 
set rowstatus =-1, LastUpdateBy = 'ADMIN', LastUpdateTime = GETDATE()
where Name IN (
'ubah_area1',
'ubah_area2',
'ubah_kota',
'ubah_model',
'ubah_propinsi',
'ubah_tipe',
'upload_daftar_harga',
'upload_daftar_warna'
)