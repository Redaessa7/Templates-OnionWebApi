﻿using Onion.Application.Interfaces.Common;

namespace Onion.Infrastructure.Mapping.Configs;

public class MappingServiceAdapter(MapsterMapper.IMapper mapper) : IMapper
{
    private readonly MapsterMapper.IMapper _mapper = mapper;

    public TDestination Map<TSource, TDestination>(TSource source)
    {
        return _mapper.Map<TDestination>(source);
    }
}