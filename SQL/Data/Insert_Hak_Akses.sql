exec up_InitPriv
@Name = 'sales_umum_master_karoseri_lihat'
, @Description = 'Sales Umum Master Karoseri Lihat'
, @Title = '2'

exec up_InitPriv
@Name = 'sales_umum_master_leasing_lihat'
, @Description = 'Sales Umum Master Leasing Lihat'
, @Title = '2'

exec up_InitPriv
@Name = 'revisi_faktur_master_revision_price_lihat'
, @Description = 'Revisi Faktur Master Revision Price Lihat'
, @Title = '1'

exec up_InitPriv
@Name = 'revisi_faktur_input'
, @Description = 'Sales Revisi Faktur Input Revisi Faktur - Buat'
, @Title = '0'

exec up_InitPriv
@Name = 'revisi_faktur_daftar_lihat'
, @Description = 'Sales Revisi Faktur Daftar Revisi Faktur - Lihat'
, @Title = '2'

exec up_InitPriv
@Name = 'revisi_faktur_daftar_konfirmasi'
, @Description = 'Sales Revisi Faktur Daftar Revisi Faktur - Konfirmasi'
, @Title = '1'

exec up_InitPriv
@Name = 'revisi_faktur_daftar_transfer'
, @Description = 'Sales Revisi Faktur Daftar Revisi Faktur - Transfer'
, @Title = '1'

exec up_InitPriv
@Name = 'revisi_faktur_daftar_edit'
, @Description = 'Sales Revisi Faktur Daftar Revisi Faktur - Edit'
, @Title = '0'


/***** RevisionPayment *****/
exec up_initPriv 'revisi_faktur_pembayaran_edit','Sales Revisi Faktur Daftar Pembayaran - Edit',2                                         
exec up_initPriv 'revisi_faktur_pembayaran_input','Sales Revisi Faktur Input Pembayaran - Buat',0                                         
exec up_initPriv 'revisi_faktur_pembayaran_konfirmasi','Sales Revisi Faktur Daftar Pembayaran - Konfirmasi',1                                   
exec up_initPriv 'revisi_faktur_pembayaran_lihat','Sales Revisi Faktur Daftar Pembayaran - Lihat',2                                         
exec up_initPriv 'revisi_faktur_pembayaran_transfer','Sales Revisi Faktur Daftar Pembayaran - Transfer',1   






